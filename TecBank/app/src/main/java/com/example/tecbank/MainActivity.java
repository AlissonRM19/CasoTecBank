package com.example.tecbank;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.os.Handler;
import android.os.Looper;
import android.text.InputType;
import android.widget.Button;
import android.widget.EditText;

import android.widget.ImageButton;

import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

import org.json.JSONObject;

import java.io.IOException;


public class MainActivity extends AppCompatActivity {

    private EditText editTextMessage;

    private EditText editTextPassword;
    private ImageButton imageButtonShowHidePassword;
    private boolean isPasswordVisible = false; // Flag para manejar el estado de visibilidad
    private boolean pantallaInicioSesionAbierta;

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        finishAffinity(); // Esto cierra todas las actividades en la pila
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        editTextMessage = findViewById(R.id.nombreusuario);

        Button buttonlogin = findViewById(R.id.login);

        //Cada vez que se abre la pantalla de inicio de secion se indica en el boolean
        pantallaInicioSesionAbierta = true;

        //Elementos de la contraseña
        editTextPassword = findViewById(R.id.password);
        imageButtonShowHidePassword = findViewById(R.id.botonojo);

        imageButtonShowHidePassword.setOnClickListener(view -> {
            if (isPasswordVisible) {
                //Cambiar a "Ocultar" contraseña
                editTextPassword.setInputType(
                        android.text.InputType.TYPE_CLASS_TEXT | android.text.InputType.TYPE_TEXT_VARIATION_PASSWORD);
                imageButtonShowHidePassword.setImageResource(R.drawable.ojo);
            }else{

                editTextPassword.setInputType(android.text.InputType.TYPE_CLASS_TEXT
                        | android.text.InputType.TYPE_TEXT_VARIATION_VISIBLE_PASSWORD);
                imageButtonShowHidePassword.setImageResource(R.drawable.botonojo);
            }
            isPasswordVisible = !isPasswordVisible; //Alternar estado
        });


        //Se escuchan los mensajes unicamente cuando la pantalla de inicio de sesion esta abierta
        new Thread(() -> {
            while (pantallaInicioSesionAbierta == true) {
                // Escuchar continuamente los mensajes del servidor
                //if (com.example.tecbank.Socket.message != null) {
                  //  procesarMensaje();
                    //textViewChat.append("Servidor: " + message + "\n");}
            }

        }).start();






       /* buttonlogin.setOnClickListener(v -> {
            String usuario = editTextMessage.getText().toString().trim();
            String password = editTextPassword.getText().toString().trim();

            if (usuario.isEmpty() || password.isEmpty()) {
                mostrarDialogo("Por favor, completa todos los campos.");
                return;
            }

            iniciarSesion(usuario, password);
        });*/

        //Button buttonlogin = findViewById(R.id.login);
        buttonlogin.setOnClickListener(view -> {
            // Aquí pasamos directo a la vista cliente sin validar
            Intent intent = new Intent(MainActivity.this, vistacliente.class);
            startActivity(intent);
            finish(); // Opcional: para que no pueda volver al login con "atrás"
        });


    }

    private void iniciarSesion(String usuario, String password) {
        OkHttpClient client = new OkHttpClient();
        MediaType JSON = MediaType.get("application/json; charset=utf-8");

        try {
            JSONObject json = new JSONObject();
            json.put("usuario", usuario);
            json.put("password", password);

            RequestBody body = RequestBody.create(json.toString(), JSON);
            Request request = new Request.Builder()
                    .url("http://45.63.12.34:5000/auth/login") // O usa el dominio de pruebas si lo necesitas
                    .post(body)
                    .build();

            // Ejecutar en segundo plano
            new Thread(() -> {
                try (Response response = client.newCall(request).execute()) {
                    String responseBody = response.body().string();

                    JSONObject respuestaJson = new JSONObject(responseBody);
                    boolean success = respuestaJson.getBoolean("success");

                    if (success) {
                        String token = respuestaJson.getString("token");

                        // Puedes guardar el token si quieres usarlo después
                        // y luego abrir otra actividad
                        runOnUiThread(() -> {
                            mostrarDialogo("Inicio de sesión exitoso");
                            Intent intent = new Intent(MainActivity.this, vistacliente.class);
                            intent.putExtra("TOKEN", token);
                            startActivity(intent);
                            finish();
                        });
                    } else {
                        runOnUiThread(() -> mostrarDialogo("Credenciales inválidas"));
                    }
                } catch (Exception e) {
                    Log.e("LOGIN_ERROR", "Error al iniciar sesión", e);
                    runOnUiThread(() -> mostrarDialogo("Ocurrió un error. Inténtalo más tarde."));
                }
            }).start();

        } catch (Exception e) {
            e.printStackTrace();
            mostrarDialogo("Error al crear la solicitud");
        }
    }

    private void mostrarDialogo(String mensaje) {
        new AlertDialog.Builder(this)
                .setTitle("TecBank")
                .setMessage(mensaje)
                .setPositiveButton("OK", null)
                .show();
    }

    // Método para cambiar el fondo de un campo a rojo temporalmente
    private void marcarCampoTemporalmente(EditText editText) {
        // Cambiar el fondo a rojo
        editText.setBackgroundResource(android.R.color.holo_red_light);

        // Restaurar el fondo blanco después de 3 segundos
        new Handler(Looper.getMainLooper()).postDelayed(() -> {
            editText.setBackgroundResource(android.R.color.white); // Reestablecer el color blanco
        }, 2000); // 2000 ms = 2 segundos
    }
}
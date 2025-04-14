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

        buttonlogin.setOnClickListener(view -> {
            String userEmail = ((EditText) findViewById(R.id.nombreusuario)).getText().toString();
            String password = ((EditText) findViewById(R.id.password)).getText().toString();
            //String usuario = editTextMessage.getText().toString();
            //String contrasena = editTextPassword.getText().toString();


            // Validar campos vacíos
            if (userEmail.isEmpty()) {
                marcarCampoTemporalmente(editTextMessage);
            }

            if (password.isEmpty()) {
                marcarCampoTemporalmente(editTextPassword);
            }

            // Continuar con la lógica solo si ambos campos están llenos
            if (!userEmail.isEmpty() && !password.isEmpty()) {
                String messageSend = "func: login, " + "userEmail: " + userEmail + ", password: " + password;
                //Socket.sendMessage(messageSend);
            }
        });


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
package com.example.tecbank;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;

import androidx.appcompat.app.AppCompatActivity;

public class vistacliente extends AppCompatActivity {

    private boolean pantallavistaclienteAbierta;


    @Override
    public void onBackPressed() {
        super.onBackPressed();
        pantallavistaclienteAbierta = false;
        Intent intent = new Intent(vistacliente.this, MainActivity.class);
        startActivity(intent);
    }

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.vistacliente);


        //Cada vez que se abre la pantalla de CasaModelo se indica en el boolean como true
        pantallavistaclienteAbierta = true;

        Button btnSalir = findViewById(R.id.btnsalir);
        btnSalir.setOnClickListener(v -> {
            finish(); // opcional, si no querés que pueda volver con el botón atrás
        });


        Button btnVerCuentas = findViewById(R.id.btnVerCuentas);
        btnVerCuentas.setOnClickListener(v -> {
            Intent intent = new Intent(vistacliente.this, VistaCuentas.class);
            startActivity(intent);
        });

        Button btnVerTarjetas = findViewById(R.id.btnVerTarjetas);
        btnVerTarjetas.setOnClickListener(v -> {
            Intent intent = new Intent(vistacliente.this, VistaTarjetas.class);
            startActivity(intent);
        });

        Button btnVerPrestamos = findViewById(R.id.btnVerPrestamos);
        btnVerPrestamos.setOnClickListener(v -> {
            Intent intent = new Intent(vistacliente.this, VistaPrestamos.class);
            startActivity(intent);
        });


    }


}
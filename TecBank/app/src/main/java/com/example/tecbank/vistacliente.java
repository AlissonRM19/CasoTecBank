package com.example.tecbank;

import android.content.Intent;
import android.os.Bundle;

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

    }


}
package com.example.tecbank;

import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;

public class VistaCuentas extends AppCompatActivity {

    private RecyclerView recyclerView;
    private MovimientoAdapter adapter;
    private ArrayList<Movimiento> listaMovimientos;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_vista_cuentas); // Esta será tu nueva vista

        recyclerView = findViewById(R.id.recyclerMovimientos);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        listaMovimientos = new ArrayList<>();
        listaMovimientos.add(new Movimiento("1234-5678-9012-3456", "Pago Recibos", "Retiro", "15/03/2025", "-$50.000"));
        listaMovimientos.add(new Movimiento("0000-0000-0000-0000", "Intereses Ganados", "Depósito", "15/05/2025", "+$5,67"));
        listaMovimientos.add(new Movimiento("1234-5678-9012-3456", "Transferencia", "Depósito", "15/03/1945", "+$8.500"));
        listaMovimientos.add(new Movimiento("0000-0000-0000-0000", "Pago Trabajos", "Retiro", "29/02/2025", "-$86.000"));

        adapter = new MovimientoAdapter(listaMovimientos);
        recyclerView.setAdapter(adapter);
    }
}
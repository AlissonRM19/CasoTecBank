package com.example.tecbank;

import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;
import java.util.List;

public class VistaPrestamos extends AppCompatActivity {

    private RecyclerView recyclerView;
    private PrestamoAdapter adapter;
    private List<Prestamo> prestamos;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.vista_prestamos);

        recyclerView = findViewById(R.id.recyclerPrestamos);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        prestamos = new ArrayList<>();
        prestamos.add(new Prestamo("Juan Pérez", "Préstamo vivienda", "$50,000", "10/04/2025", "5%", "$52,500"));
        prestamos.add(new Prestamo("Ana Gómez", "Préstamo auto", "$20,000", "08/04/2025", "8%", "$21,600"));

        adapter = new PrestamoAdapter(prestamos);
        recyclerView.setAdapter(adapter);
    }
}

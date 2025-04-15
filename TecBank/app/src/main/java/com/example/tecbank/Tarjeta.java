package com.example.tecbank;

public class Tarjeta {
    private String numero;
    private String descripcion;
    private String fecha;
    private String total;

    public Tarjeta(String numero, String descripcion, String fecha, String total) {
        this.numero = numero;
        this.descripcion = descripcion;
        this.fecha = fecha;
        this.total = total;
    }

    public String getNumero() {
        return numero;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public String getFecha() {
        return fecha;
    }

    public String getTotal() {
        return total;
    }


}
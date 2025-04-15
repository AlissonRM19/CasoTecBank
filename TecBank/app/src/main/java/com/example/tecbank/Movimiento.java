package com.example.tecbank;

public class Movimiento {
    private String numeroTarjeta;
    private String descripcion;
    private String tipoTransferencia;
    private String fecha;
    private String totalTransferido;

    public Movimiento(String numeroTarjeta, String descripcion, String tipoTransferencia, String fecha, String totalTransferido) {
        this.numeroTarjeta = numeroTarjeta;
        this.descripcion = descripcion;
        this.tipoTransferencia = tipoTransferencia;
        this.fecha = fecha;
        this.totalTransferido = totalTransferido;
    }

    public String getNumeroTarjeta() { return numeroTarjeta; }
    public String getDescripcion() { return descripcion; }
    public String getTipoTransferencia() { return tipoTransferencia; }
    public String getFecha() { return fecha; }
    public String getTotalTransferido() { return totalTransferido; }
}
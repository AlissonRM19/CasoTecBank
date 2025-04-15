package com.example.tecbank;

public class Prestamo {
    private String nombre;
    private String descripcion;
    private String cantidad;
    private String fecha;
    private String intereses;
    private String total;

    public Prestamo(String nombre, String descripcion, String cantidad, String fecha, String intereses, String total) {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.cantidad = cantidad;
        this.fecha = fecha;
        this.intereses = intereses;
        this.total = total;
    }

    public String getNombre() { return nombre; }
    public String getDescripcion() { return descripcion; }
    public String getCantidad() { return cantidad; }
    public String getFecha() { return fecha; }
    public String getIntereses() { return intereses; }
    public String getTotal() { return total; }

    // setters para editar (si los llegás a necesitar)
    public void setNombre(String nombre) { this.nombre = nombre; }
    public void setDescripcion(String descripcion) { this.descripcion = descripcion; }
    public void setCantidad(String cantidad) { this.cantidad = cantidad; }
    public void setFecha(String fecha) { this.fecha = fecha; }
    public void setIntereses(String intereses) { this.intereses = intereses; }
    public void setTotal(String total) { this.total = total; }
}

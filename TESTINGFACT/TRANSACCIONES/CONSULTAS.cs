using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TESTINGFACT.TRANSACCIONES
{
    public class CONSULTAS
    {
        #region ARTICULO SERVICIO
        public string AGREGAR_ARTICULO_SERVICIO(string CODIGO, string CLASE, string DESCRIPCION, string UNIDAD, double PRECIO_SIN_IVA, bool GENERA_IVA, float P_IEPS, bool GENERA_ISH, int STOCK_MINIMO, int STOCK_MAXIMO, string ANAQUEL)
        {
            string sentencia = "INSERT INTO ARTICULO_SERVICIO(CODIGO,CLASE,DESCRIPCION,UNIDAD,PRECIO_SIN_IVA,GENERA_IVA,P_IEPS,GENERA_ISH,STOCK_MINIMO,STOCK_MAXIMO,ANAQUEL)VALUES" +
                "('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},'{10}')";
            string res = string.Format(sentencia,
                CODIGO, CLASE, DESCRIPCION, UNIDAD, PRECIO_SIN_IVA, (GENERA_IVA ? 1 : 0), P_IEPS, (GENERA_ISH ? 1 : 0), STOCK_MINIMO, STOCK_MAXIMO, ANAQUEL);
            return res;
        }
        public string MODIFICAR_ARTICULO_SERVICIO(string CODIGO, string CLASE, string DESCRIPCION, string UNIDAD, double PRECIO_SIN_IVA, bool GENERA_IVA, float P_IEPS, bool GENERA_ISH, int STOCK_MINIMO, int STOCK_MAXIMO, string ANAQUEL)
        {
            string sentencia = "UPDATE ARTICULO_SERVICIO SET CLASE='{0}',DESCRIPCION='{1}',UNIDAD='{2}',PRECIO_SIN_IVA={3},GENERA_IVA={4},P_IEPS={5},P_ISH={6},STOCK_MINIMO={7},STOCK_MAXIMO={8},ANAQUEL='{9}'"
                + " WHERE CODIGO='{10}'";
            string res = string.Format(sentencia,
                CLASE, DESCRIPCION, UNIDAD, PRECIO_SIN_IVA, (GENERA_IVA ? 1 : 0), P_IEPS, (GENERA_ISH ? 1 : 0), STOCK_MINIMO, STOCK_MAXIMO, ANAQUEL, CODIGO);
            return res;
        }
        public string EXISTE_ARTICULO_SERVICIO(string CODIGO)
        {
            string sentencia = "IF EXISTS(SELECT CODIGO FROM ARTICULO_SERVICIO WHERE CODIGO='{0}') SELECT 1; ELSE SELECT 0;";
            string res = string.Format(sentencia, CODIGO);
            return res;
        }
        public string OBTENER_ARTICULO_SERVICIO_POR_CODIGO(string CODIGO)
        {
            string sentencia = "SELECT CODIGO,DESCRIPCION,PRECIO_SIN_IVA,UNIDAD,GENERA_IVA,GENERA_ISH,P_IEPS FROM ARTICULO_SERVICIO WHERE CODIGO = '{0}'";
            string res = string.Format(sentencia, CODIGO);
            return res;
        }
        public string OBTENER_ARTICULO_SERVICIO_POR_DESCRIPCION(string DESCRIPCION, int ELEMENTOS)
        {
            string sentencia = "SELECT TOP {0} CODIGO,DESCRIPCION,PRECIO_SIN_IVA,UNIDAD,GENERA_IVA,GENERA_ISH,P_IEPS FROM ARTICULO_SERVICIO WHERE DESCRIPCION LIKE '%{1}%'";
            string res = string.Format(sentencia, ELEMENTOS, DESCRIPCION);
            return res;
        }
        #endregion

        #region CLIENTE
        public string AGREGAR_CLIENTE(string RFC, string RAZON_SOCIAL, string CALLE, string NUMERO_EXTERIOR, string NUMERO_INTERIOR, string COLONIA, string DELEGACION_MUNICIPIO, string LOCALIDAD, string ESTADO, string PAIS, string CODIGO_POSTAL, string CORREO_ELECTRONICO)
        {
            string sentencia = "INSERT INTO CLIENTE(RFC,RAZON_SOCIAL,CALLE,NUMERO_EXTERIOR,NUMERO_INTERIOR,COLONIA,DELEGACION_MUNICIPIO,LOCALIDAD,ESTADO,PAIS,CODIGO_POSTAL,CORREO_ELECTRONICO)VALUES" +
                "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
            string res = string.Format(sentencia,
                RFC, RAZON_SOCIAL, CALLE, NUMERO_EXTERIOR, NUMERO_INTERIOR, COLONIA, DELEGACION_MUNICIPIO, LOCALIDAD, ESTADO, PAIS, CODIGO_POSTAL, CORREO_ELECTRONICO);
            return res;
        }
        public string MODIFICAR_CLIENTE(string RFC, string RAZON_SOCIAL, string CALLE, string NUMERO_EXTERIOR, string NUMERO_INTERIOR, string COLONIA, string DELEGACION_MUNICIPIO, string LOCALIDAD, string ESTADO, string PAIS, string CODIGO_POSTAL, string CORREO_ELECTRONICO)
        {
            string sentencia = "UPDATE CLIENTE SET RAZON_SOCIAL='{0}',CALLE='{1}',NUMERO_EXTERIOR='{2}',NUMERO_INTERIOR='{3}',COLONIA='{4}',DELEGACION_MUNICIPIO='{5}',LOCALIDAD='{6}',ESTADO='{7}',PAIS='{8}',CODIGO_POSTAL='{9}',CORREO_ELECTRONICO='{10}' WHERE RFC='{11}'";
            string res = string.Format(sentencia,
                RAZON_SOCIAL, CALLE, NUMERO_EXTERIOR, NUMERO_INTERIOR, COLONIA, DELEGACION_MUNICIPIO, LOCALIDAD, ESTADO, PAIS, CODIGO_POSTAL, CORREO_ELECTRONICO, RFC);
            return res;
        }
        public string EXISTE_CLIENTE(string RFC)
        {
            string sentencia = "IF EXISTS(SELECT RFC FROM CLIENTE WHERE RFC='{0}') SELECT 1; ELSE SELECT 0;";
            string res = string.Format(sentencia, RFC);
            return res;
        }
        public string OBTENER_CLIENTE_POR_RFC(string RFC)
        {
            string sentencia = "SELECT RFC,RAZON_SOCIAL,CALLE,NUMERO_INTERIOR,NUMERO_EXTERIOR,COLONIA,DELEGACION_MUNICIPIO,LOCALIDAD,ESTADO,PAIS,CODIGO_POSTAL,CORREO_ELECTRONICO FROM CLIENTE WHERE RFC = '{0}'";
            string res = string.Format(sentencia, RFC);
            return res;
        }
        public string OBTENER_CLIENTE_POR_RAZON_SOCIAL(string RAZON_SOCIAL, int ELEMENTOS)
        {
            string sentencia = "SELECT TOP {1} RFC,RAZON_SOCIAL,CALLE,NUMERO_INTERIOR,NUMERO_EXTERIOR,COLONIA,DELEGACION_MUNICIPIO,LOCALIDAD,ESTADO,PAIS,CODIGO_POSTAL,CORREO_ELECTRONICO FROM CLIENTE WHERE RAZON_SOCIAL LIKE '%{0}%'";
            string res = string.Format(sentencia, RAZON_SOCIAL, ELEMENTOS);
            return res;
        }
        #endregion

        #region NOTAS DE VENTA
        public string AGREGAR_NOTA_DE_VENTA()
        {
            string sentencia = "INSERT INTO NOTA_DE_VENTA(FECHA_DE_REGISTRO)VALUES(GETDATE()); SELECT TOP 1 ID FROM NOTA_DE_VENTA ORDER BY ID DESC;";
            string res = sentencia;
            return res;
        }
        #endregion

        #region VENTAS
        public string AGREGAR_VENTAS(int ID_NOTA_DE_VENTA, string CODIGO, float CANTIDAD, string UNIDAD, string DESCRIPCION, double PRECIO, double P_IEPS, double P_IVA, double P_ISH)//, double IMPORTE)
        {
            string sentencia = "INSERT INTO VENTAS(ID_NOTA_DE_VENTA,CODIGO,CANTIDAD,UNIDAD,DESCRIPCION,PRECIO,IMPORTE,IVA,IEPS,ISH)VALUES(" +
                "{0},'{1}',{2},'{3}','{4}',{5},{6},{7},{8},{9})";
            string res = string.Format(sentencia, ID_NOTA_DE_VENTA, CODIGO, CANTIDAD, UNIDAD, DESCRIPCION, PRECIO, (CANTIDAD * PRECIO), ((CANTIDAD * PRECIO) * P_IVA), ((CANTIDAD * PRECIO) * P_IEPS), ((CANTIDAD * PRECIO) * P_ISH));
            return res;
        }
        #endregion

        #region FACTURAS
        public string AGREGAR_FACTURA(string OBSERVACIONES, string FORMA_DE_PAGO, double SUBTOTAL, int ID_NOTA_DE_VENTA, string RFC_CLIENTE, string FOLIO_DE_FACTURA,
            string RAZON_SOCIAL, string MONEDA, double TOTAL, string METODO_DE_PAGO)
        {
            string sentencia = "INSERT INTO FACTURA(FECHA_DE_FACTURACION,FECHA_DE_REGISTRO,FORMA_DE_PAGO,SUBTOTAL,ID_NOTA_DE_VENTA,RFC_CLIENTE,FOLIO_DE_FACTURA,RAZON_SOCIAL,MONEDA,TOTAL,METODO_DE_PAGO)VALUES(" +
                "GETDATE(),GETDATE(),'{0}',{1},{2},'{3}','{4}','{5}','{6}',{7},'{8}');SELECT MAX(ID) FROM FACTURA";
            string res = string.Format(sentencia, FORMA_DE_PAGO, SUBTOTAL, ID_NOTA_DE_VENTA,
                    RFC_CLIENTE, FOLIO_DE_FACTURA, RAZON_SOCIAL, MONEDA, TOTAL, METODO_DE_PAGO);
            return res;
        }
        public string ACTUALIZAR_fACTURA(int ID_FACTURA, string TIMBRADO)
        {
            string sentencia = "UPDATE FACTURA SET TIMBRADO='{0}' WHERE ID={1}";
            string res = string.Format(sentencia, TIMBRADO, ID_FACTURA);
            return res;
        }
        #endregion

        #region FACTURACIONES
        public string AGREGAR_FACTURACION(int ID_NOTA_DE_VENTA, int ID_FACURA)//, double IMPORTE)
        {
            string sentencia = "INSERT INTO FACTURACIONES(CODIGO,CANTIDAD,UNIDAD,DESCRIPCION,PRECIO,IMPORTE,IEPS,IVA,ISH,ID_FACTURA)SELECT CODIGO,CANTIDAD,UNIDAD,DESCRIPCION,PRECIO,IMPORTE,IEPS,IVA,ISH,{0} FROM VENTAS WHERE ID_NOTA_DE_VENTA={1}";
            string res = string.Format(sentencia, ID_FACURA, ID_NOTA_DE_VENTA);
            return res;
        }
        #endregion
    }
}
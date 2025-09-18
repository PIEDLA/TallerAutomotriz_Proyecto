
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lib_dominio.Entidades
{
    public class Repuestos
    {
        public int Id { get; set; }

        public int Id_proveedor { get; set; }
        [ForeignKey("Id_proveedor")] public Proveedores? _Proveedor { get; set; }

        public string? Nombre_repuesto { get; set; }
        public string? Marca { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

    }
}

<<<<<<< HEAD

=======
<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> c6f6a0d98f2d99a6e17d5a421e765e5227782ecf
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> main


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

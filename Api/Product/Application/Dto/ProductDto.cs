﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InkaPharmacy.Api.Product.Application.Dto
{
    public class ProductDto
    {
        public  long Id { get; set; }
        public  string Name { get; set; }
        public  decimal Price { get; set; }
        public  string Currency { get; set; }
        public  int Stock { get; set; }
        public  long Category_id { get; set; }
        public  string Lot_number { get; set; }
        public  string Sanitary_registration_number { get; set; }
        public  DateTime Registration_date { get; set; }
        public  DateTime Expiration_date { get; set; }
        public  int Status { get; set; }
        public  int Stock_status { get; set; }

    }
}
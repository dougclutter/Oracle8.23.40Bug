﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BugCode.Models;

public partial class InvoiceDetail
{
    public decimal Id { get; set; }

    public decimal InvoiceId { get; set; }

    public string ItemId { get; set; }

    public decimal? Amount { get; set; }

    public virtual Invoice Invoice { get; set; }
}
﻿using Ams.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ams.ViewModels
{
    public class ReceivableVm
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public int ReceivableLedger { get; set; }

        public string remarks { get; set; }
      
       
        public List<Ledger> ledgers;
        public SelectList ReceivableLegerSelectList()
        {
            var FilterReceivableLedger = ledgers.Where(x => x.Parent_ledgerId == 0).ToList();
            return new SelectList(
                FilterReceivableLedger,
                nameof(Ledger.Id),
                nameof(Ledger.Ledger_name),
                ReceivableLedger

                );
        }
     
    }
}

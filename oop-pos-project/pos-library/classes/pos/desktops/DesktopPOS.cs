﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pos_library.abstracts;
using pos_library.interfaces;
using pos_library.tools;
using pos_library;

namespace pos_library.classes.pos.desktops
{
    public class DesktopPOS : APOSMachine, IDesktopPOS
    {
        public DesktopPOS(string machineName, string ipAddress, string version, string location):base(machineName, ipAddress)
        {
            POSLocation = location;
            Version = version;
            SetPOSType(Constants.POS_TYPE_DESKTOP);
        }
        public string POSLocation { get; set; }

        public override void AddTransaction(ATransaction transaction)
        {
            UITools.Print($"-Adding transaction {transaction.TransactionNumber} at the BASE {POSType}");
            _transactions.Add(transaction);
        }

        public override List<ATransaction> GetTransactions()
        {
            return _transactions;
        }

        public override void ClearTransactions()
        {
            base.ClearTransactions();
        }

        public override void PrintPOSDetail()
        {
            UITools.Print($"This desktop POS with version {Version} is located in {POSLocation}");
        }

        public override void SummarizeTransactions()
        {
            var totalSale = 0M;
            var voidedSaleCount = 0;
            var voidedNoSaleCount = 0;
            var totalNoSale = 0M;
            foreach(var tran in _transactions)
            {
                if (tran.TranType == Constants.TranTypes.SALE)
                {
                    if (!tran.IsVoided)
                    {
                        totalSale += tran.TransactionTotal;
                    }
                    else
                    {
                        voidedSaleCount++;
                    }
                }
                else
                {
                    if (!tran.IsVoided)
                    {
                        totalNoSale += tran.TransactionTotal;
                    }
                    else
                    {
                        voidedNoSaleCount++;
                    }
                }
            }

            UITools.Print("---------------- SUMMARY ----------------");
            UITools.Print($"Total Number of Transactions : {_transactions.Count}");
            UITools.Print($"----------------------------------------");
            UITools.Print($"Total Sale Amount : {totalSale}");
            UITools.Print($"Total No Sale Amount : {totalNoSale}");
            UITools.Print($"----------------------------------------");
            UITools.Print($"Voided Sale : {voidedSaleCount}");
            UITools.Print($"Voided No Sale : {voidedNoSaleCount}");
            UITools.Print("-----------------------------------------");
            UITools.Print($"Source : {POSType} - {IPAddress}");
            UITools.Print("-----------------------------------------");
        }
    }
}

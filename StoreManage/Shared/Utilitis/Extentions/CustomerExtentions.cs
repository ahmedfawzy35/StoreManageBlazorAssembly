using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Utilitis.Extentions
{
    public static class CustomerExtentions
    {
        public static List<CustomerDto> ToCustomerDto(this List<Customer> source)
        {
            List<CustomerDto> data = new List<CustomerDto>();
            foreach (var item in source) {

                data.Add(new CustomerDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Adress = item.Adress,
                    BrancheId = item.BrancheId,
                    BrancheName = item.Branche is null ? "" : item.Branche.Name,
                    CustomertypeId = item.CustomertypeId,
                    CustomerTypeName = item.Customertype is null ? "" : item.Customertype.Name,
                    StartAccount = item.StartAccount,
                    StopDealing = item.StopDealing,
                    CustomerAccount = item.CustomerAccount == null ? 0 : item.CustomerAccount
                });
            }
            return data;
        }
    }
}

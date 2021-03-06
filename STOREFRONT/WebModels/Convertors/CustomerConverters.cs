﻿#region
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using Newtonsoft.Json.Linq;
using Omu.ValueInjecter;
using VirtoCommerce.ApiClient.DataContracts.CustomerService;
using VirtoCommerce.ApiClient.DataContracts.Security;
using VirtoCommerce.Web.Models;
using VirtoCommerce.Web.Models.FormModels;
using Data = VirtoCommerce.ApiClient.DataContracts.CustomerService;

#endregion

namespace VirtoCommerce.Web.Convertors
{
    public static class CustomerConverters
    {
        #region Public Methods and Operators
        public static Data.Address AsServiceModel(this CustomerAddress customerAddress)
        {
            var address = new Data.Address();

            address.City = customerAddress.City;
            address.CountryCode = customerAddress.CountryCode;
            address.CountryName = customerAddress.Country;
            address.FirstName = customerAddress.FirstName;
            address.LastName = customerAddress.LastName;
            address.Line1 = customerAddress.Address1;
            address.Line2 = customerAddress.Address2;
            address.Name = string.Format("{0} {1}", customerAddress.FirstName, customerAddress.LastName);
            address.Organization = customerAddress.Company;
            address.Phone = customerAddress.Phone;
            address.PostalCode = customerAddress.Zip;
            address.RegionName = customerAddress.Province;
            address.RegionId = customerAddress.ProvinceCode;
            address.Zip = customerAddress.Zip;

            return address;
        }

        public static Customer AsWebModel(this Contact contact)
        {
            var customer = new Customer();

            int index = 1;
            foreach (var address in contact.Addresses)
            {
                var customerAddress = address.AsWebModel();
                customerAddress.Id = index.ToString();

                customer.Addresses.Add(customerAddress);
                index++;
            }

            if (!string.IsNullOrEmpty(contact.FullName))
            {
                var splittedName = contact.FullName.Split(' ');

                customer.FirstName = splittedName[0];
                customer.LastName = splittedName[1];
            }

            customer.DefaultAddress = customer.Addresses.FirstOrDefault();

            customer.AcceptsMarketing = true; // TODO
            customer.Email = contact.Emails.FirstOrDefault();
            customer.HasAccount = true; // TODO
            customer.Id = contact.Id;

            if (contact.DynamicProperties != null && contact.DynamicProperties.Any())
            {
                customer.DynamicProperties = contact.DynamicProperties.Select(x => x.ToViewModel()).ToList();
            }
            return customer;
        }

        public static CustomerAddress AsWebModel(this Data.Address address)
        {
            var customerAddress = new CustomerAddress();

            customerAddress.Address1 = address.Line1;
            customerAddress.Address2 = address.Line2;
            customerAddress.City = address.City;
            customerAddress.Company = address.Organization;
            customerAddress.Country = address.CountryName;
            customerAddress.CountryCode = "US";
            customerAddress.FirstName = address.FirstName;
            customerAddress.LastName = address.LastName;
            customerAddress.Phone = address.Phone;
            customerAddress.Province = address.RegionName;
            customerAddress.ProvinceCode = address.RegionId;
            customerAddress.Zip = address.PostalCode ?? address.Zip;

            return customerAddress;
        }

        public static CustomerAddress AsWebModel(this NewAddressFormModel formModel)
        {
            var customerAddress = new CustomerAddress();

            customerAddress.Address1 = formModel.Address1;
            customerAddress.Address2 = formModel.Address2;
            customerAddress.City = formModel.City;
            customerAddress.Company = formModel.Company;
            customerAddress.Country = formModel.Country;
            customerAddress.CountryCode = "US";
            customerAddress.FirstName = formModel.FirstName;
            customerAddress.LastName = formModel.LastName;
            customerAddress.Phone = formModel.Phone;
            customerAddress.Province = formModel.Province;
            customerAddress.ProvinceCode = formModel.ProvinceCode;
            customerAddress.Zip = formModel.Zip;

            return customerAddress;
        }

        public static CustomerAddress AsWebModel(this CustomerAddressFormModel formModel)
        {
            var customerAddress = new CustomerAddress();

            customerAddress.Address1 = formModel.Address1;
            customerAddress.Address2 = formModel.Address2;
            customerAddress.City = formModel.City;
            customerAddress.Company = formModel.Company;
            customerAddress.Country = formModel.Country;
            customerAddress.CountryCode = "US";
            customerAddress.FirstName = formModel.FirstName;
            customerAddress.Id = formModel.Id;
            customerAddress.LastName = formModel.LastName;
            customerAddress.Phone = formModel.Phone;
            customerAddress.Province = formModel.Province;
            customerAddress.ProvinceCode = formModel.ProvinceCode;
            customerAddress.Zip = formModel.Zip;

            return customerAddress;
        }
        #endregion

    }
}
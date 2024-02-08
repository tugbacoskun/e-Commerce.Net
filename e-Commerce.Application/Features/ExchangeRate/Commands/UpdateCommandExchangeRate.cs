using AutoMapper;
using e_Commerce.Application.Interfaces;
using e_Commerce.Application.Response;
using e_Commerce.Domain.Enum;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace e_Commerce.Application.Features.ExchangeRate.Commands
{
    public class UpdateCommandExchangeRate : IRequestHandler<UpdateCommandRequestExchangeRate, bool>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public UpdateCommandExchangeRate(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<bool> Handle(UpdateCommandRequestExchangeRate request, CancellationToken cancellationToken)
        {
            try
            {
                XmlDocument xmlVerisi = new XmlDocument();
                xmlVerisi.Load("http://www.tcmb.gov.tr/kurlar/today.xml");

                decimal usdValue = Math.Round(Convert.ToDecimal(xmlVerisi.SelectSingleNode($"Tarih_Date/Currency[@Kod='USD']/ForexSelling").InnerText.Replace('.', ',')), 2);
                decimal eurValue = Math.Round(Convert.ToDecimal(xmlVerisi.SelectSingleNode($"Tarih_Date/Currency[@Kod='EUR']/ForexSelling").InnerText.Replace('.', ',')), 2);

                var exchangeRateUsd = await _exchangeRateRepository.FirstOrDefaultAsync(x => x.CurrencyTypeId == CurrencyTypeLookup.USD);
                if (exchangeRateUsd != null)
                {
                    exchangeRateUsd.Value = usdValue;
                }
                else
                {
                    exchangeRateUsd = new Domain.Entities.ExchangeRate { CurrencyTypeId = CurrencyTypeLookup.USD, Value = usdValue };
                    await _exchangeRateRepository.AddAsync(exchangeRateUsd);
                }

                var exchangeRateEur = await _exchangeRateRepository.FirstOrDefaultAsync(x => x.CurrencyTypeId == CurrencyTypeLookup.EUR);
                if (exchangeRateEur != null)
                {
                    exchangeRateEur.Value = eurValue;
                }
                else
                {
                    exchangeRateEur = new Domain.Entities.ExchangeRate { CurrencyTypeId = CurrencyTypeLookup.EUR, Value = eurValue };
                    await _exchangeRateRepository.AddAsync(exchangeRateEur);
                }

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                return false;
            }
        }


    }
}

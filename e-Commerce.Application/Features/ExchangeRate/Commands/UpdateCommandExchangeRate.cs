using AutoMapper;
using e_Commerce.Application.Response;
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
        private readonly IeCommerceDbContext _context;

        public UpdateCommandExchangeRate(IeCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCommandRequestExchangeRate request, CancellationToken cancellationToken)
        {
            try
            {
                XmlDocument xmlVerisi = new XmlDocument();
                xmlVerisi.Load("http://www.tcmb.gov.tr/kurlar/today.xml");
                decimal usdValue = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ','));
                decimal eurValue = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "EUR")).InnerText.Replace('.', ','));

                // USD ExchangeRate nesnesini kontrol et ve gerekirse güncelle veya ekle
                var exchangeRateUsd = await _context.ExchangeRates.FirstOrDefaultAsync(x => x.CurrencyTypeId == Domain.Enum.CurrencyTypeLookup.USD);
                if (exchangeRateUsd != null)
                {
                    exchangeRateUsd.Value = usdValue;
                }
                else
                {
                    exchangeRateUsd = new Domain.Entities.ExchangeRate { CurrencyTypeId = Domain.Enum.CurrencyTypeLookup.USD, Value = usdValue };
                    await _context.ExchangeRates.AddAsync(exchangeRateUsd);
                }

                // EUR ExchangeRate nesnesini kontrol et ve gerekirse güncelle veya ekle
                var exchangeRateEur = await _context.ExchangeRates.FirstOrDefaultAsync(x => x.CurrencyTypeId == Domain.Enum.CurrencyTypeLookup.EUR);
                if (exchangeRateEur != null)
                {
                    exchangeRateEur.Value = eurValue;
                }
                else
                {
                    exchangeRateEur = new Domain.Entities.ExchangeRate { CurrencyTypeId = Domain.Enum.CurrencyTypeLookup.EUR, Value = eurValue };
                    await _context.ExchangeRates.AddAsync(exchangeRateEur);
                }

                // Değişiklikleri kaydet
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını logla
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                return false;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApi.DataModel;

namespace WebApi.Models
{
    public class PersonellerDTO
    {
        public int PersonelID { get; set; }
        [Required(ErrorMessage = "Adı Zorunlu Alan")]
        public string Adi { get; set; }
        [Required(ErrorMessage = "Soyadı Zorunlu Alan")]
        public string Soyadi { get; set; }
        public string Sehir { get; set; }
        public string Bolge { get; set; }
        [StringLength(5, ErrorMessage = "Girdiğiniz Ülkenin Kısaltmasını Kullanınız", MinimumLength = 2)]
        public string Ulke { get; set; }
        public bool IsActive { get; set; }

        public static List<PersonellerDTO> ConvertToPersonelDTOs(List<Personeller> personel)
        {
            List<PersonellerDTO> personellerDTOs = new List<PersonellerDTO>();
            foreach (Personeller p in personel)
            {
                PersonellerDTO personellerDTO = new PersonellerDTO();
                personellerDTO.Adi = p.Adi ?? " ";
                personellerDTO.PersonelID = p.PersonelID;
                personellerDTO.Soyadi = p.SoyAdi ?? " ";
                personellerDTO.Ulke = p.Ulke ?? " ";
                personellerDTO.IsActive = p.IsActive;

                personellerDTOs.Add(personellerDTO);
            }
            return personellerDTOs;
        }

        public static PersonellerDTO ConvertToPersonelDTO(Personeller personel)
        {
            PersonellerDTO personellerDTO = new PersonellerDTO();
            personellerDTO.Adi = personel.Adi ?? " ";
            personellerDTO.PersonelID = personel.PersonelID;
            personellerDTO.Soyadi = personel.SoyAdi ?? " ";
            personellerDTO.Ulke = personel.Ulke ?? " ";
            personellerDTO.Sehir = personel.Sehir ?? " ";
            personellerDTO.Bolge = personel.Bolge ?? " ";
            personellerDTO.IsActive = personel.IsActive;
            return personellerDTO;
        }

        public static Personeller ConvertToPersonel(PersonellerDTO personeldto, Personeller personel = null)
        {
            if (personeldto.PersonelID > 0)// yeni personel
            {

                if (personel == null)
                {
                    throw new Exception("Girilen Id ye Sahip Personel Bulunmamaktadır.");
                }
                personel.Adi = personeldto.Adi ?? personel.Adi;
                personel.PersonelID = personeldto.PersonelID;
                personel.SoyAdi = personeldto.Soyadi ?? personel.SoyAdi;
                personel.Ulke = personeldto.Ulke ?? personel.Ulke;
                personel.IsActive = true;
                personel.Sehir = personeldto.Sehir ?? personel.Sehir;
                personel.Bolge = personeldto.Bolge ?? personel.Bolge;

                return personel;
            }
            else//güncelleme
            {
                Personeller yenipersonel = new Personeller();
                yenipersonel.Adi = personeldto.Adi ?? null;
                yenipersonel.PersonelID = personeldto.PersonelID;
                yenipersonel.SoyAdi = personeldto.Soyadi ?? null;
                yenipersonel.Ulke = personeldto.Ulke ?? null;
                yenipersonel.IsActive = true;
                yenipersonel.Sehir = personeldto.Sehir ?? null;
                yenipersonel.Bolge = personeldto.Bolge ?? null;
                return yenipersonel;

            }

        }
    }
}
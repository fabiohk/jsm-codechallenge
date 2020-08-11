using System;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace JSMCodeChallenge.Helpers
{
    public static class Parser
    {
        public static User parseUserDTO(UserDTO userDTO, string phoneRegion, string defaultNationality)
        {
            Name name = new Name()
            {
                Title = userDTO.Name?.Title,
                First = userDTO.Name?.First,
                Last = userDTO.Name?.Last
            };
            Coordinates coordinates = new Coordinates()
            {
                Latitude = Convert.ToDecimal(userDTO.Location?.Coordinates?.Latitude),
                Longitude = Convert.ToDecimal(userDTO.Location?.Coordinates?.Longitude)
            };
            Timezone timezone = new Timezone()
            {
                Offset = userDTO.Location?.Timezone?.Offset,
                Description = userDTO.Location?.Timezone?.Description
            };
            Location location = new BrazilianLocation()
            {
                Street = userDTO.Location?.Street,
                City = userDTO.Location?.City,
                PostalCode = userDTO.Location?.PostalCode,
                State = userDTO.Location?.State,
                Coordinates = coordinates,
                Timezone = timezone
            };
            Picture picture = new Picture()
            {
                Large = userDTO.Picture?.Large,
                Medium = userDTO.Picture?.Medium,
                Thumbnail = userDTO.Picture?.Thumbnail
            };

            return new User()
            {
                Email = userDTO.Email,
                Gender = userDTO.Gender,
                Name = name,
                Phones = PhoneUtil
                    .RetrieveValidPhoneNumbers(new List<string>() { userDTO.Phone }, phoneRegion)
                    .Select(PhoneUtil.ConvertToE164Format),
                CellPhones = PhoneUtil
                    .RetrieveValidPhoneNumbers(new List<string>() { userDTO.CellPhone }, phoneRegion)
                    .Select(PhoneUtil.ConvertToE164Format),
                RegisteredDate = userDTO.Registered?.Date,
                BirthDate = userDTO.Birth?.Date,
                Picture = picture,
                Nationality = defaultNationality,
            };
        }
    }
}
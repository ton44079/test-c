using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TestC.Models;
using TestC.Repositories.Interface;
using TestC.Services.Interface;

namespace TestC.Services.Implements
{
    public class PropService : IPropService
    {
        private IPropertiesRepository _propRepsitory;

        public PropService(IPropertiesRepository propRepsitory)
        {
            _propRepsitory = propRepsitory;
        }

        private bool CheckRoomMinValue(string targetString, decimal val)
        {
            string onlyNumber = Regex.Match(targetString, @"\d+").Value;

            if (decimal.TryParse(onlyNumber, out decimal convertVal))
            {
                return convertVal >= val;
            }
            else
            {
                return false;
            }
        }


        private bool CheckRoomMaxValue(string targetString, decimal val)
        {
            string onlyNumber = Regex.Match(targetString, @"\d+").Value;
            if (decimal.TryParse(onlyNumber, out decimal convertVal))
            {
                return convertVal <= val;
            }
            else
            {
                return false;
            }
        }

        public IList<PropModel> Search(ParametersModel model)
        {
            List<PropModel> result = new List<PropModel>();
            var draft = _propRepsitory.FindAll().ToList().AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.uniq_id))
            {
                draft = draft.Where(e => e.uniq_id.ToUpper() == model.uniq_id.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(model.property_type))
            {
                draft = draft.Where(e => e.property_type.ToUpper().Contains(model.property_type.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(model.city))
            {
                draft = draft.Where(e => e.city.ToUpper().Contains(model.city.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(model.amenities))
            {
                draft = draft.Where(e => e.amenities.ToUpper().Contains(model.amenities.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(model.room_price))
            {
                decimal? minVal = null;
                decimal? maxVal = null;
                var arrStr = model.room_price.Split('-');
                if (decimal.TryParse(arrStr[0], out decimal convertMin))
                {
                    minVal = convertMin;
                }

                if (arrStr.Count() > 1)
                {
                    if (decimal.TryParse(arrStr[1], out decimal convertMax))
                    {
                        maxVal = convertMax;
                    }
                }

                if (minVal.HasValue)
                {
                    draft = draft.Where(e => CheckRoomMinValue(e.room_price, minVal.Value));
                }

                if (maxVal.HasValue)
                {
                    draft = draft.Where(e => CheckRoomMaxValue(e.room_price, maxVal.Value));
                }
            }

            if (!string.IsNullOrWhiteSpace(model.location))
            {
                decimal? lat = null;
                decimal? lng = null;
                var arrStr = model.location.Split(',');
                if (decimal.TryParse(arrStr[0], out decimal convertMin))
                {
                    lat = convertMin;
                }

                if (arrStr.Count() > 1)
                {
                    if (decimal.TryParse(arrStr[1], out decimal convertMax))
                    {
                        lng = convertMax;
                    }
                }

                if (lat.HasValue)
                {
                    draft = draft.Where(e => e.latitude == lat.Value);
                }

                if (lng.HasValue)
                {
                    draft = draft.Where(e => e.longitude == lng.Value);
                }
            }


            result = draft.Select(e => new PropModel()
            {
                uniq_id = e.uniq_id,
                property_type = e.property_type,
                city = e.city,
                amenities = e.amenities,
                room_price = e.room_price,
                location = $"{e.latitude},{e.longitude}"
            }).ToList();

            return result;
        }
    }
}
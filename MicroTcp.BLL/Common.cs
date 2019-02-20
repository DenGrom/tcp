﻿using MicroTcp.BLL.Models;
using MicroTcp.DAL.Entities;
using MicroTcp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL
{
    public class Common
    {
        public ClientRepository _clientRepository;
        public Common()
        {
            _clientRepository = new ClientRepository();
        }
        public bool ValidatePortNumber(string portNumber)
        {
            var portNumberParsed = 0;
            var isPortNumberValid = Int32.TryParse(portNumber, out portNumberParsed)
                && (portNumberParsed >= 5555 && portNumberParsed <= 5564);
            return isPortNumberValid;
        }

        public ValidatePortModel ValidatePortNumberTuple(string portNumber)
        {
            var portNumberParsed = 0;
            var isPortNumberValid = Int32.TryParse(portNumber, out portNumberParsed)
                && (portNumberParsed >= 5555 && portNumberParsed <= 5564);
            return new ValidatePortModel
            {
                IsValid = isPortNumberValid
                //PortNmber = portNumberParsed
            };
        }

        public Client SignIn(string nickName, string password)
        {
            var client = _clientRepository.SignIn(nickName, password);
            return client;
        }
    }
}

﻿using PainAssessment.Areas.Admin.Data;
using PainAssessment.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PainAssessment.Areas.Admin.Services
{
    public class PractitionerService : IPractitionerService
    {
        internal IUnitOfWork unitOfWork;
        public PractitionerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void CreatePractitioner(Practitioner practitioner)
        {
            unitOfWork.PractitionerGateway.Add(practitioner);
        }

        public void DeletePractitioner(Guid id)
        {
            unitOfWork.PractitionerGateway.Delete(id);
        }

        public IEnumerable<Practitioner> GetAllPractitioners()
        {
            return unitOfWork.PractitionerGateway.GetAll();
        }

        public Practitioner GetPractitioner(Guid id)
        {
            return unitOfWork.PractitionerGateway.FindById(id);
        }

        public void SavePractitioner()
        {
            unitOfWork.Save();
        }

        public void UpdatePractitioner(Practitioner practitioner)
        {
            unitOfWork.PractitionerGateway.Update(practitioner);
        }

        //public IEnumerable<Practitioner> GetAllPractitionersByPage(int page = 1)
        //{

        //    return GetAllPractitioners().ToList().ChunkBy(8)[page - 1];
        //}
        public IEnumerable<Practitioner> GetAllPractitionersByPageAndName(int page = 1, string name = "")
        {
            return GetAllPractitioners().Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList().ChunkBy(8)[page - 1];
        }
        public int GetPractitionersCount()
        {
            return GetAllPractitioners().Count();
        }
    }
}

//public static class ListExtensions
//{
//    public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
//    {
//        return source
//            .Select((x, i) => new { Index = i, Value = x })
//            .GroupBy(x => x.Index / chunkSize)
//            .Select(x => x.Select(v => v.Value).ToList())
//            .ToList();
//    }
//}

//public static class IEnumerableExtensions
//{
//    public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
//    {
//        for (int i = 0; i < source.Count(); i += chunkSize)
//            yield return source.Skip(i).Take(chunkSize);
//    }
//}
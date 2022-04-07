using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PainAssessment.Interfaces;
using PainAssessment.Areas.ModuleTwo.Models;
using PainAssessment.Models;
using System.Collections.Generic;

namespace PainAssessment.Domain
{
    public class TemplateChecklistAdapter: ITemplateChecklistAdapter
    {
        private readonly Areas.ModuleTwo.Services.IChecklistService checklistser;

        public TemplateChecklistAdapter(Areas.ModuleTwo.Services.IChecklistService checklistservice)
        {
            checklistser = checklistservice;
        }

        public void UpdateActive(int checklistID)
        {

            Checklist checklist = checklistser.GetById(checklistID);

            Checklist newChecklist = copyChecklistForInsertion(checklist);


                if (newChecklist.Active)
                {
                    newChecklist.Active = false;
                 
                }
                else
                {
                    newChecklist.Active = true;
                }
            checklistser.Delete(checklist);
            checklistser.Insert(newChecklist);
      
        }

        public void EditTemplate(int checklistID, string name, string description, bool active)
        {

            Checklist checklist = checklistser.GetById(checklistID);

            Checklist newChecklist = copyChecklistForInsertion(checklist);


            newChecklist.ChecklistName = name;
            newChecklist.ChecklistDescription = description;
            newChecklist.Active = active;
            checklistser.Delete(checklist);
            checklistser.Insert(newChecklist);

        }

        public void AddQuestion(int checklistID, string subDomain, string determinant, int domain, int maxWeightage)
        {
            Checklist currChecklist = checklistser.GetById(checklistID);

            Checklist newChecklist = copyChecklistForInsertion(currChecklist);
         
            switch(domain)
            {
                case 0:
                    // Central
                    CentralDomain newCenQn = new CentralDomain();
                    newCenQn.SubDomain = subDomain;
                    newCenQn.MaxValue = maxWeightage;
                    newCenQn.Determinant = determinant;
                    newChecklist.Central.Add(newCenQn);
                    break;
                case 1:
                    // Regional
                    RegionalDomain newRegQn = new RegionalDomain();
                    newRegQn.SubDomain = subDomain;
                    newRegQn.MaxValue = maxWeightage;
                    newRegQn.Determinant = determinant;
                    newChecklist.Regional.Add(newRegQn);
                    break;
                case 2:
                    // Local
                    LocalDomain newLocQn = new LocalDomain();
                    newLocQn.SubDomain = subDomain;
                    newLocQn.MaxValue = maxWeightage;
                    newLocQn.Determinant = determinant;
                    newChecklist.Local.Add(newLocQn);
                    break;
                default:
                    // Smth wrong
                    break;
            }

            // Delete old and insert a new one due to service limitations
            checklistser.Delete(currChecklist);
            checklistser.Insert(newChecklist);
        }

        /// <summary>
        /// Helper function to copy a current checklist.
        /// This is required due to limitatinos on the service.
        /// </summary>
        /// <param name="originalChecklist"></param>
        /// <returns></returns>
        private Checklist copyChecklistForInsertion(Checklist originalChecklist)
        {
            Checklist newChecklist = checklistser.InitialiseChecklist();

            #region Checks to prevent empty domain lists
            if(originalChecklist.Central.Count != 0)
            {
                newChecklist.Central.RemoveAt(0);
            }
            if(originalChecklist.Regional.Count() != 0)
            {
                newChecklist.Regional.RemoveAt(0);
            }
            if(originalChecklist.Local.Count() != 0)
            {
                newChecklist.Local.RemoveAt(0);
            }
            #endregion

            // Copy checklist details
            newChecklist.ChecklistName = originalChecklist.ChecklistName;
            newChecklist.ChecklistDescription = originalChecklist.ChecklistDescription;
            newChecklist.PractitionerId = originalChecklist.PractitionerId;
            newChecklist.Active = originalChecklist.Active;

            #region Deep copy Central domain
            List<CentralDomain> tempCentral = new List<CentralDomain>();
            foreach(var item in originalChecklist.Central)
            {
                // Need to rebuild these
                if(item.Checklist != null)
                {
                    CentralDomain temp = new CentralDomain();
                    temp.Determinant = item.Determinant;
                    temp.MaxValue = item.MaxValue;
                    temp.SubDomain = item.SubDomain;
                    tempCentral.Add(temp);
                }
            }

            foreach(var item in tempCentral)
            {
                newChecklist.Central.Add(item);
            }
            #endregion

            #region Deep copy Regional domain
            List<RegionalDomain> tempRegional = new List<RegionalDomain>();
            foreach(var item in originalChecklist.Regional)
            {
                // Need to rebuild these
                if(item.Checklist != null)
                {
                    RegionalDomain temp = new RegionalDomain();
                    temp.Determinant = item.Determinant;
                    temp.MaxValue = item.MaxValue;
                    temp.SubDomain = item.SubDomain;
                    tempRegional.Add(temp);
                }
            }
            //currChecklist.Regional.RemoveRange(0, currChecklist.Regional.Count());
            foreach(var item in tempRegional)
            {
                newChecklist.Regional.Add(item);
            }
            #endregion

            #region Deep copy Local domain
            List<LocalDomain> tempLocal = new List<LocalDomain>();
            foreach(var item in originalChecklist.Local)
            {
                // Need to rebuild these
                if(item.Checklist != null)
                {
                    LocalDomain temp = new LocalDomain();
                    temp.Determinant = item.Determinant;
                    temp.MaxValue = item.MaxValue;
                    temp.SubDomain = item.SubDomain;
                    tempLocal.Add(temp);
                }
            }
            //currChecklist.Local.RemoveRange(0, currChecklist.Local.Count());
            foreach(var item in tempLocal)
            {
                newChecklist.Local.Add(item);
            }
            #endregion

            return newChecklist;
        }

        /// <summary>
        ///  Helper function due to updating requiring to delete and then inserting the entire checklist,
        ///  causing the checklist ID to change
        /// </summary>
        /// <returns></returns>
        public int GetRecentlyModifiedChecklist()
        {
            List<Checklist> allChecklist = checklistser.GetAll(1);

            // Find highest ID
            int i = 0;
            foreach(var item in allChecklist)
            {
                if(item.ChecklistId > i)
                {
                    i = item.ChecklistId;
                }
            }
            return i;
        }

        public void UpdateQuestion(int checklistID, string subDomain, string determinant, int domain, int maxWeightage, int rowId)
        {
            Checklist currChecklist = checklistser.GetById(checklistID);

            Checklist newChecklist = copyChecklistForInsertion(currChecklist);

            switch(domain)
            {
                case 0:
                    // Central
                    foreach(var item in currChecklist.Central)
                    {
                        if(item.RowId == rowId)
                        {
                            foreach(var tem in newChecklist.Central)
                            {
                                if(tem.SubDomain == item.SubDomain && tem.Determinant == item.Determinant && tem.MaxValue == item.MaxValue)
                                {
                                    tem.SubDomain = subDomain;
                                    tem.Determinant = determinant;
                                    tem.MaxValue = maxWeightage;
                                }
                            }

                        }
                    }
                    break;
                case 1:
                    // Regional
                    foreach(var item in currChecklist.Regional)
                    {
                        if(item.RowId == rowId)
                        {
                            foreach(var tem in newChecklist.Regional)
                            {
                                if(tem.SubDomain == item.SubDomain && tem.Determinant == item.Determinant && tem.MaxValue == item.MaxValue)
                                {
                                    tem.SubDomain = subDomain;
                                    tem.Determinant = determinant;
                                    tem.MaxValue = maxWeightage;
                                    
                                }
                            }

                        }
                    }
                    break;
                case 2:
                    // Local
                    foreach(var item in currChecklist.Local)
                    {
                        if(item.RowId == rowId)
                        {
                            foreach(var tem in newChecklist.Local)
                            {
                                if(tem.SubDomain == item.SubDomain && tem.Determinant == item.Determinant && tem.MaxValue == item.MaxValue)
                                {
                                    tem.SubDomain = subDomain;
                                    tem.Determinant = determinant;
                                    tem.MaxValue = maxWeightage;
                                }
                            }

                        }
                    }
                    break;
                default:
                    // Smth wrong
                    break;
            }

            // Delete old and insert a new one due to service limitations
            checklistser.Delete(currChecklist);
            checklistser.Insert(newChecklist);
        }

        public void DeleteQuestion(int checklistID, string subDomain, string determinant, int domain, int maxWeightage, int rowId)
        {
            Checklist currChecklist = checklistser.GetById(checklistID);

            Checklist newChecklist = copyChecklistForInsertion(currChecklist);

            switch(domain)
            {
                case 0:
                    // Central
                    foreach(var item in currChecklist.Central)
                    {
                        if(item.RowId == rowId)
                        {
                            CentralDomain temp = null;
                            foreach(var tem in newChecklist.Central)
                            {
                                if(tem.SubDomain == item.SubDomain && tem.Determinant == item.Determinant && tem.MaxValue == item.MaxValue)
                                {
                                    temp = tem;
                                }
                            }
                            if(temp != null)
                            {
                                newChecklist.Central.Remove(temp);
                            }

                        }
                    }
                    break;
                case 1:
                    // Regional
                    foreach(var item in currChecklist.Regional)
                    {
                        if(item.RowId == rowId)
                        {
                            RegionalDomain temp = null;
                            foreach(var tem in newChecklist.Regional)
                            {
                                if(tem.SubDomain == item.SubDomain && tem.Determinant == item.Determinant && tem.MaxValue == item.MaxValue)
                                {
                                    temp = tem;
                                }
                            }
                            if(temp != null)
                            {
                                newChecklist.Regional.Remove(temp);
                            }

                        }
                    }
                    break;
                case 2:
                    // Local
                    foreach(var item in currChecklist.Local)
                    {
                        if(item.RowId == rowId)
                        {
                            LocalDomain temp = null;
                            foreach(var tem in newChecklist.Local)
                            {
                                if(tem.SubDomain == item.SubDomain && tem.Determinant == item.Determinant && tem.MaxValue == item.MaxValue)
                                {
                                    temp = tem;
                                }
                            }
                            if(temp != null)
                            {
                                newChecklist.Local.Remove(temp);
                            }

                        }
                    }
                    break;
                default:
                    // Smth wrong
                    break;
            }

            // Delete old and insert a new one due to service limitations
            checklistser.Delete(currChecklist);
            checklistser.Insert(newChecklist);
        }

        public void DeleteChecklist(int checklistID)
        {
            Checklist checklistToDelete = this.checklistser.GetById(checklistID);
            this.checklistser.Delete(checklistToDelete);
        }

        public Areas.ModuleTwo.Models.Checklist GetChecklistByChecklistID(int checklistID)
        {
            return this.checklistser.GetById(checklistID);
        }

        public List<Areas.ModuleTwo.Models.Checklist> GetAllAdminChecklists()
        {
            // This is hardcoded due to the service limitations
            return this.checklistser.GetAll(1);
        }

        public void InsertNewChecklist(Areas.ModuleTwo.Models.Checklist checklist)
        {
            this.checklistser.Insert(checklist);
        }

        public Areas.ModuleTwo.Models.Checklist InitialiseChecklist()
        {
            return this.checklistser.InitialiseChecklist();
        }
    }

}

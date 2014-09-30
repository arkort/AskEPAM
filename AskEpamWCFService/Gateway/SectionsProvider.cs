using AskEpamEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskEpamWCFService.Gateway
{
    public class SectionsProvider
    {
        public static List<QuestionSection> ListSections()
        {
            List<QuestionSection> sections = new List<QuestionSection>();

            AskEpamDB_LINQDataContext context = new AskEpamDB_LINQDataContext();
            List<Section> allUserSections = context.Sections.ToList();

            foreach (Section userSection in allUserSections)
            {
                sections.Add(new QuestionSection(userSection.id,userSection.title));
            }

            return sections;
        }
    }
}
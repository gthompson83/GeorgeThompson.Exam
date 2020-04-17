using System;
using System.Collections.Generic;
using System.Linq;

namespace GeorgeThompson.Exam
{
    public class Testlet
    {
        public string TestletId;
        private List<Item> Items;
        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }
        public List<Item> Randomize()
        {
            // Items private collection has 6 Operational and 4 Pretest Items.
            // Randomize the order of these items as per the requirement(with TDD)
            // The assignment will be reviewed on the basis of – Tests written first, Correct
            // logic, Well structured &clean readable code.
            var finalResult = new List<Item>();
            var randos = Items.Where(x => x.ItemType == ItemTypeEnum.Pretest).OrderBy(o => Guid.NewGuid()).Take(AppSettings.PretestItemCount).ToList();

            // We have 2 of our 10 items selected above. 
            // The requirement states there is a fixed set of (10) items.
            // Depending on how that constraint is enforced, by the constructor/Item repository/whatever, we may want to add a "Take(8)" to ensure only 10 items are added. 

            // var finalEight = Items.Where(x => !randos.Contains(x)).OrderBy(o => Guid.NewGuid()).Take(AppSettings.MixedItemCount).ToList();

            // But hopefully these numbers are not hardcoded but pulled from site.config or the database


            var finalEight = Items.Where(x => !randos.Contains(x)).OrderBy(o => Guid.NewGuid()).ToList();

            finalResult.AddRange(randos);
            finalResult.AddRange(finalEight);

            Items = finalResult;
            return Items;
        }


    }
}

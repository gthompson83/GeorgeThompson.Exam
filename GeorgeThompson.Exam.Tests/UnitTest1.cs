using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeorgeThompson.Exam.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private List<Item> LoadItems()
        {
            var list = new List<Item>()
            {
                new Item {ItemId = "1", ItemType = ItemTypeEnum.Pretest},
                new Item {ItemId = "2", ItemType = ItemTypeEnum.Pretest},
                new Item {ItemId = "3", ItemType = ItemTypeEnum.Pretest},
                new Item {ItemId = "4", ItemType = ItemTypeEnum.Pretest},
                new Item {ItemId = "5", ItemType = ItemTypeEnum.Operational},
                new Item {ItemId = "6", ItemType = ItemTypeEnum.Operational},
                new Item {ItemId = "7", ItemType = ItemTypeEnum.Operational},
                new Item {ItemId = "8", ItemType = ItemTypeEnum.Operational},
                new Item {ItemId = "9", ItemType = ItemTypeEnum.Operational},
                new Item {ItemId = "10", ItemType = ItemTypeEnum.Operational}
            };

            return list;
        }


        [TestMethod]
        public void Assert_First_Two_Items_Are_Pretest()
        {
            var items = LoadItems();
            var testlet = new Testlet("Testlet1", items);

            var firstTwoItems = testlet.Randomize().Take(2).ToList();

            Assert.IsTrue(firstTwoItems.All(x => x.ItemType == ItemTypeEnum.Pretest));
        }

        [TestMethod]
        public void Assert_Last_Eight_Items_Are_Pretest_and_Operational()
        {
            var items = LoadItems();
            var testlet = new Testlet("Testlet2", items);

            var lastEightItems = testlet.Randomize().Skip(2).Take(8).ToList();

            Assert.IsTrue(lastEightItems.Any(x => x.ItemType == ItemTypeEnum.Operational));
            Assert.IsTrue(lastEightItems.Any(x => x.ItemType == ItemTypeEnum.Pretest));
        }

        [TestMethod]
        public void Assert_Items_are_Randomized()
        {
            var items = LoadItems();
            var testlet = new Testlet("Testlet2", items);

            var availableItemIds = new List<string> { "1", "2", "3", "4" };

            LoopMethod(testlet, testlet.Randomize().Take(2).ToList(), availableItemIds);

            if (availableItemIds.Any())
            {
                testlet.Randomize();
                LoopMethod(testlet, testlet.Randomize().Take(2).ToList(), availableItemIds);
            }

            Assert.IsTrue(availableItemIds.Count == 0);

        }


        private void LoopMethod(Testlet testlet, List<Item> firstTwoItems, List<string> availableItemIds)
        {
            foreach (var item in firstTwoItems)
            {
                if (availableItemIds.Contains(item.ItemId))
                {
                    availableItemIds.Remove(item.ItemId);
                }
            }
            if (availableItemIds.Any())
            {
                LoopMethod(testlet, testlet.Randomize().Take(2).ToList(), availableItemIds);
            }
        }
    }
}

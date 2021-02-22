using UnityEngine;

using System.Collections.Generic;
using Serializable = System.SerializableAttribute;

namespace BreadAndButter.Loot
{
    [CreateAssetMenu(menuName = "Bread and Butter/Loot/LootTable", fileName = "NewLootTable")]
    public class LootTable : ScriptableObject
    {
        [Serializable]
        public class WeightedLoot
        {
            [SerializeField, Range(1, 100)]
            protected int weighting = 50;
            [SerializeField]
            protected Lootable loot;

            public void AddLootToTable(ref List<Lootable> _table)
            {
                // Add as many copies of the loot as weighting into the table
                for(int i = 0; i < weighting; i++)
                {
                    _table.Add(loot);
                }
            }
        }

        [SerializeField]
        private WeightedLoot[] possibleLoot;

        private List<Lootable> table = new List<Lootable>();

        /// <summary>
        /// Clears and fills the loot table list with loot.
        /// </summary>
        public void GenerateTable()
        {
            // Clear the table to ensure new loot is put in
            table.Clear();

            // Fill the table with the weighted loots from the possible list
            foreach(WeightedLoot loot in possibleLoot)
            {
                loot.AddLootToTable(ref table);
            }
        }

        /// <summary>
        /// Fills a contents list with loot based on the amount count passed.
        /// </summary>
        /// <param name="_count">How many items that are being added to the table.</param>
        public void FillContents(ref List<Lootable> _contents, int _count)
        {
            // Generate as many loot items as passed and add them to the contents
            for(int i = 0; i < _count; i++)
            {
                _contents.Add(GenerateLoot());
            }
        }

        /// <summary>
        /// Grabs a random item from the loot table and returns it.
        /// If the table hasn't been filled, it will automatically be filled.
        /// </summary>
        public Lootable GenerateLoot()
        {
            // If the table is empty, fill it
            if(table.Count == 0)
            {
                foreach(WeightedLoot loot in possibleLoot)
                {
                    loot.AddLootToTable(ref table);
                }
            }

            // Return a random lootable from the loot table
            return table[Random.Range(0, table.Count - 1)];
        }
    }
}
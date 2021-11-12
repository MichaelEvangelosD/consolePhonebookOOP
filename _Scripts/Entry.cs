enum EntryState
{
    Empty,
    Used,
}

namespace Phonebook_F3_OOP
{
    class Entry
    {
        private int entryIdx;
        public int EntryIndex { get => entryIdx; set => entryIdx = value; }

        private string name;
        public string Name { get => name; set => name = value; }

        private long number;
        public long Number { get => number; set => number = value; }

        private EntryState entryState;
        public EntryState EntryState { get => entryState; set => entryState = value; }

        /// <summary>
        /// Use this constructor only for creating an empty entry
        /// with EntryState of Empty
        /// </summary>
        public Entry()
        {
            EntryIndex = 0;
            Name = "emptyString";
            Number = 0;
            EntryState = EntryState.Empty;
        }

        /// <summary>
        /// The correct-to-use constructor, completely build the Entry obj 
        /// and set its EntryState to Used
        /// </summary>
        /// <param name="entryIdx">The object numerical ID</param>
        /// <param name="name">The object string Name</param>
        /// <param name="number">The object numerical value to represent a phone number</param>
        public Entry(int entryIdx, string name, long number)
        {
            EntryIndex = entryIdx;
            Name = name;
            Number = number;
            EntryState = EntryState.Used;
        }

        public string GetEntryInfo()
        {
            return $"Entry #{EntryIndex}\t{Name}\t{Number}";
        }
    }
}

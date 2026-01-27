using System.Collections.Generic;
namespace Utils {

    public class Safe2D<TKey, TVal> : Dictionary<TKey, List<TVal>> {
        public new List<TVal> this[TKey key] {
            get {
                if (!ContainsKey(key)) Add(key, new List<TVal>());
                return base[key];
            } set => base[key] = value;
        }
        public void RemoveAt(TKey key, int index) {
            if (!ContainsKey(key)) return;
            this[key].RemoveAt(index);
            if (this[key].Count == 0) Remove(key);
        }
        public void Remove(TKey key, TVal val) {
            if (!ContainsKey(key)) return;
            this[key].Remove(val);
            if (this[key].Count == 0) Remove(key);
        }
    }

    public class Safe2D<TKey0, TKey1, TVal> : Dictionary<TKey0, Dictionary<TKey1, TVal>>  {
        public new Dictionary<TKey1, TVal> this[TKey0 key] {
            get {
                if (!ContainsKey(key)) Add(key, new Dictionary<TKey1, TVal>());
                return base[key];
            } set => base[key] = value;
        }
        public void Remove(TKey0 key0, TKey1 key1) {
            if (!ContainsKey(key0)) return;
            this[key0].Remove(key1);
            if (this[key0].Count == 0) Remove(key0);
        }
    }
}

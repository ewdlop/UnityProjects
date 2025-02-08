> keyequalityComparer;

    #region Events
    ProxyEventBlock<KeyValuePair<K, V>> eventBlock;

    /// <summary>
    /// The change event. Will be raised for every change operation on the collection.
    /// </summary>
    public override event CollectionChangedHandler<KeyValuePair<K, V>> CollectionChanged
    {
      add { (eventBlock ?? (eventBlock = new ProxyEventBlock<KeyValuePair<K, V>>(this, pairs))).CollectionChanged += value; }
      remove { if (eventBlock != null) eventBlock.CollectionChanged -= value; }
    }

    /// <summary>
    /// The change event. Will be raised for every change operation on the collection.
    /// </summary>
    public override event CollectionClearedHandler<KeyValuePair<K, V>> CollectionCleared
    {
      add { (eventBlock ?? (eventBlock = new ProxyEventBlock<KeyValuePair<K, V>>(this, pairs))).CollectionCleared += value; }
      remove { if (eventBlock != null) eventBlock.CollectionCleared -= value; }
    }

    /// <summary>
    /// The item added  event. Will be raised for every individual addition to the collection.
    /// </summary>
    public override event ItemsAddedHandler<KeyValuePair<K, V>> ItemsAdded
    {
      add { (eventBlock ?? (eventBlock = new ProxyEventBlock<KeyValuePair<K, V>>(this, pairs))).ItemsAdded += value; }
      remove { if (eventBlock != null) eventBlock.ItemsAdded -= value; }
    }

    /// <summary>
    /// The item added  event. Will be raised for every individual removal from the collection.
    /// </summary>
    public override event ItemsRemovedHandler<KeyValuePair<K, V>> ItemsRemoved
    {
      add { (eventBlock ?? (eventBlock = new ProxyEventBlock<KeyValuePair<K, V>>(this, pairs))).ItemsRemoved += value; }
      remove { if (eventBlock != null) eventBlock.ItemsRemoved -= value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public override EventTypeEnum ListenableEvents
    {
      get
      {
        return EventTypeEnum.Basic;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public override EventTypeEnum ActiveEvents
    {
      get
      {
        return pairs.ActiveEvents;
      }
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyequalityComparer"></param>
    protected DictionaryBase(SCG.IEqualityComparer<K> keyequalityComparer)
    {
      if (keyequalityComparer == null)
        throw new NullReferenceException("Key equality comparer cannot be null");
      this.keyequalityComparer = keyequalityComparer;
    }

    #region IDictionary<K,V> Members

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public virtual SCG.IEqualityComparer<K> EqualityComparer { get { return keyequalityComparer; } }


    /// <summary>
    /// Add a new (key, value) pair (a mapping) to the dictionary.
    /// </summary>
    /// <exception cref="DuplicateNotAllowedException"> if there already is an entry with the same key. </exception>
    /// <param name="key">Key to add</param>
    /// <param name="value">Value to add</param>
    [Tested]
    public virtual void Add(K key, V value)
    {
      KeyValuePair<K, V> p = new KeyValuePair<K, V>(key, value);

      if (!pairs.Add(p))
        throw new DuplicateNotAllowedException("Key being added: '" + key + "'");
    }

    /// <summary>
    /// Add the entries from a collection of <see cref="T:C5.KeyValuePair`2"/> pairs to this dictionary.
    /// <para><b>TODO: add restrictions L:K and W:V when the .Net SDK allows it </b></para>
    /// </summary>
    /// <exception cref="DuplicateNotAllowedException"> 
    /// If the input contains duplicate keys or a key already present in this dictionary.</exception>
    /// <param name="entries"></param>
    public virtual void AddAll<L, W>(SCG.IEnumerable<KeyValuePair<L, W>> entries)
      where L : K
      where W : V
    {
      foreach (KeyValuePair<L, W> pair in entries)
      {
        KeyValuePair<K, V> p = new KeyValuePair<K, V>(pair.Key, pair.Value);
        if (!pairs.Add(p))
          throw new DuplicateNotAllowedException("Key being added: '" + pair.Key + "'");
      }
    }

    /// <summary>
    /// Remove an entry with a given key from the dictionary
    /// </summary>
    /// <param name="key">The key of the entry to remove</param>
    /// <returns>True if an entry was found (and removed)</returns>
    [Tested]
    public virtual bool Remove(K key)
    {
      KeyValuePair<K, V> p = new KeyValuePair<K, V>(key);

      return pairs.Remove(p);
    }


    /// <summary>
    /// Remove an entry with a given key from the dictionary and report its value.
    /// </summary>
    /// <param name="key">The key of the entry to remove</param>
    /// <param name="value">On exit, the value of the removed entry</param>
    /// <returns>True if an entry was found (and removed)</returns>
    [Tested]
    public virtual bool Remove(K key, out V value)
    {
      KeyValuePair<K, V> p = new KeyValuePair<K, V>(key);

      if (pairs.Remove(p, out p))
      {
        value = p.Value;
        return true;
      }
      else
      {
        value = default(V);
        return false;
      }
    }


    /// <summary>
    /// Remove all entries from the dictionary
    /// </summary>
    [Tested]
    public virtual void Clear() { pairs.Clear(); }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public virtual Speed ContainsSpeed { get { return pairs.ContainsSpeed; } }

    /// <summary>
    /// Check if there is an entry with a specified key
    /// </summary>
    /// <param name="key">The key to look for</param>
    /// <returns>True if key was found</returns>
    [Tested]
    public virtual bool Contains(K key)
    {
      KeyValuePair<K, V> p = new KeyValuePair<K, V>(key);

      return pairs.Contains(p);
    }

    [Serializable]
    class LiftedEnumerable<H> : SCG.IEnumerable<KeyValuePair<K, V>> where H : K
    {
      SCG.IEnumerable<H> keys;
      public LiftedEnumerable(SCG.IEnumerable<H> keys) { this.keys = keys; }
      public SCG.IEnumerator<KeyValuePair<K, V>> GetEnumerator() { foreach (H key in keys) yield return new KeyValuePair<K, V>(key); }

      #region IEnumerable Members

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
        throw new Exception("The method or operation is not implemented.");
      }

      #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public virtual bool ContainsAll<H>(SCG.IEnumerable<H> keys) where H : K
    {
      return pairs.ContainsAll(new LiftedEnumerable<H>(keys));
    }

    /// <summary>
    /// Check if there is an entry with a specified key and report the corresponding
    /// value if found. This can be seen as a safe form of "val = this[key]".
    /// </summary>
    /// <param name="key">The key to look for</param>
    /// <param name="value">On exit, the value of the entry</param>
    /// <returns>True if key was found</returns>
    [Tested]
    public virtual bool Find(K key, out V value)
    {
      return Find(ref key, out value);
    }
    /// <summary>
    /// Check if there is an entry with a specified key and report the corresponding
    /// value if found. This can be seen as a safe form of "val = this[key]".
    /// </summary>
    /// <param name="key">The key to look for</param>
    /// <param name="value">On exit, the value of the entry</param>
    /// <returns>True if key was found</returns>
    public virtual bool Find(ref K key, out V value)
    {
      KeyValuePair<K, V> p = new KeyValuePair<K, V>(key);

      if (pairs.Find(ref p))
      {
        key = p.Key;
        value = p.Value;
        return true;
      }
      else
      {
        value = default(V);
        return false;
      }
    }


    /// <summary>
    /// Look for a specific key in the dictionary and if found replace the value with a new one.
    /// This can be seen as a non-adding version of "this[key] = val".
    /// </summary>
    /// <param name="key">The key to look for</param>
    /// <param name="value">The new value</param>
    /// <returns>True if key was found</returns>
    [Tested]
    public virtual bool Update(K key, V value)
    {
      KeyValuePair<K, V> p = new KeyValuePair<K, V>(key, value);

      return pairs.Update(p);
    }


    /// <summ
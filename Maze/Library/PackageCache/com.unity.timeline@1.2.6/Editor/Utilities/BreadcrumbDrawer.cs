cording to the
    /// itemequalityComparer to a particular value. If so, return in the ref argument (a
    /// binary copy of) the actual value found.
    /// </summary>
    /// <param name="item">The value to look for.</param>
    /// <returns>True if the items is in this collection.</returns>
    [Tested]
    public bool Find(ref T item)
    {
      int ind;

      if (binarySearch(item, out ind))
      {
        item = array[ind];
        return true;
      }

      return false;
    }


    //This should probably just be bool Add(ref T item); !!!
    /// <summary>
    /// Check if this collection contains an item equivalent according to the
    /// itemequalityComparer to a particular value. If so, return in the ref argument (a
    /// binary copy of) the actual value found. Else, add the item to the collection.
    /// </summary>
    /// <param name="item">The value to look for.</param>
    /// <returns>True if the item was added (hence not found).</returns>
    [Tested]
    public bool FindOrAdd(ref T item)
    {
      updatecheck();

      int ind;

      if (binarySearch(item, out ind))
      {
        item = array[ind];
        return true;
      }

      if (size == array.Length) expand();

      Array.Copy(array, ind, array, ind + 1, size - ind);
      array[ind] = item;
      size++;
      raiseForAdd(item);
      return false;
    }


    /// <summary>
    /// Check if this collection contains an item equivalent according to the
    /// itemequalityComparer to a particular value. If so, update the item in the collection 
    /// to with a binary copy of the supplied value. If the collection has bag semantics,
    /// it is implementation dependent if this updates all equivalent copies in
    /// the collection or just one.
    /// </summary>
    /// <param name="item">Value to update.</param>
    /// <returns>True if the item was found and hence updated.</returns>
    [Tested]
    public bool Update(T item)
    { T olditem; return Update(item, out olditem); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="olditem"></param>
    /// <returns></returns>
    public bool Update(T item, out T olditem)
    {
      updatecheck();

      int ind;

      if (binarySearch(item, out ind))
      {
        olditem = array[ind];
        array[ind] = item;
        raiseForUpdate(item, olditem);
        return true;
      }

      olditem = default(T);
      return false;
    }


    /// <summary>
    /// Check if this collection contains an item equivalent according to the
    /// itemequalityComparer to a particular value. If so, update the item in the collection 
    /// to with a binary copy of the supplied value; else add the value to the collection. 
    /// </summary>
    /// <param name="item">Value to add or update.</param>
    /// <returns>True if the item was found and updated (hence not added).</returns>
    [Tested]
    public bool UpdateOrAdd(T item)
    { T olditem; return UpdateOrAdd(item, out olditem); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="olditem"></param>
    /// <returns></returns>
    public bool UpdateOrAdd(T item, out T olditem)
    {
      updatecheck();

      int ind;

      if (binarySearch(item, out ind))
      {
        olditem = array[ind];
        array[ind] = item;
        raiseForUpdate(item, olditem);
        return true;
      }

      if (size == array.Length) expand();

      Array.Copy(array, ind, array, ind + 1, size - ind);
      array[ind] = item;
      size++;
      olditem = default(T);
      raiseForAdd(item);
      return false;
    }


    /// <summary>
    /// Remove a particular item from this collection. If the collection has bag
    /// semantics only one copy equivalent to the supplied item is removed. 
    /// </summary>
    /// <param name="item">The value to remove.</param>
    /// <returns>True if the item was found (and removed).</returns>
    [Tested]
    public bool Remove(T item)
    {
      int ind;

      updatecheck();
      if (binarySearch(item, out ind))
      {
        T removeditem = array[ind];
        Array.Copy(array, ind + 1, array, ind, size - ind - 1);
        array[--size] = default(T);
        raiseForRemove(removeditem);
        return true;
      }

      return false;
    }


    /// <summary>
    /// Remove a particular item from this collection if found. If the collection
    /// has bag semantics only one copy equivalent to the supplied item is removed,
    /// which one is implementation dependent. 
    /// If an item was removed, report a binary copy of the actual item removed in 
    /// the argument.
    /// </summary>
    /// <param name="item">The value to remove.</param>
    /// <param name="removeditem">The removed value.</param>
    /// <returns>True if the item was found (and removed).</returns>
    [Tested]
    public bool Remove(T item, out T removeditem)
    {
      int ind;

      updatecheck();
      if (binarySearch(item, out ind))
      {
        removeditem = array[ind];
        Array.Copy(array, ind + 1, array, ind, size - ind - 1);
        array[--size] = default(T);
        raiseForRemove(removeditem);
        return true;
      }

      removeditem = default(T);
      return false;
    }


    /// <summary>
    /// Remove all items in another collection from this one. 
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="items">The items to remove.</param>
    [Tested]
    public void RemoveAll<U>(SCG.IEnumerable<U> items) where U : T
    {
      //This is O(m*logn) with n bits extra storage
      //(Not better to collect the m items and sort them)
      updatecheck();
        
      RaiseForRemoveAllHandler raiseHandler = new RaiseForRemoveAllHandler(this);
      bool mustFire = raiseHandler.MustFire;

      int[] toremove = new int[(size >> 5) + 1];
      int ind, j = 0;

      foreach (T item in items)
        if (binarySearch(item, out ind))
          toremove[ind >> 5] |= 1 << (ind & 31);

      for (int i = 0; i < size; i++)
        if ((toremove[i >> 5] & (1 << (i & 31))) == 0)
          array[j++] = array[i];
        else if (mustFire)
          raiseHandler.Remove(array[i]);

      Array.Clear(array, j, size - j);
      size = j;
      if (mustFire)
        raiseHandler.Raise();
    }

    /// <summary>
    /// Remove all items not in some other collection from this one. 
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="items">The items to retain.</param>
    [Tested]
    public void RetainAll<U>(SCG.IEnumerable<U> items) where U : T
    {
      //This is O(m*logn) with n bits extra storage
      //(Not better to collect the m items and sort them)
      updatecheck();

      RaiseForRemoveAllHandler raiseHandler = new RaiseForRemoveAllHandler(this);
      bool mustFire = raiseHandler.MustFire;

      int[] toretain = new int[(size >> 5) + 1];
      int ind, j = 0;

      foreach (T item in items)
        if (binarySearch(item, out ind))
          toretain[ind >> 5] |= 1 << (ind & 31);

      for (int i = 0; i < size; i++)
        if ((toretain[i >> 5] & (1 << (i & 31))) != 0)
          array[j++] = array[i];
        else if (mustFire)
          raiseHandler.Remove(array[i]);

      Array.Clear(array, j, size - j);
      size = j;
      if (mustFire)
        raiseHandler.Raise();
    }

    /// <summary>
    /// Check if this collection contains all the values in another collection.
    /// Multiplicities are not taken into account.
    /// </summary>
    /// <param name="items">The </param>
    /// <typeparam name="U"></typeparam>
    /// <returns>True if all values in <code>items</code>is in this collection.</returns>
    [Tested]
    public bool ContainsAll<U>(SCG.IEnumerable<U> items) where U : T
    {
      int tmp;

      foreach (T item in items)
        if (!binarySearch(item, out tmp))
          return false;

      return true;
    }


    /// <summary>
    /// Count the number of items of the collection equal to a particular value.
    /// Returns 0 if and only if the value is not in the collection.
    /// </summary>
    /// <param name="item">The value to count.</param>
    /// <returns>The number of copies found (0 or 1).</returns>
    [Tested]
    public int ContainsCount(T item)
    {
      int tmp;

      return binarySearch(item, out tmp) ? 1 : 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual ICollectionValue<T> UniqueItems() { return this; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual ICollectionValue<KeyValuePair<T, int>> ItemMultiplicities()
    {
      return new MultiplicityOne<T>(this);
    }

    /// <summary>
    /// Remove all (0 or 1) items equivalent to a given value.
    /// </summary>
    /// <param name="item">The value to remove.</param>
    [Tested]
    public void RemoveAllCopies(T item) { Remove(item); }


    /// <summary>
    /// Check the integrity of the internal data structures of this collection.
    /// Only avaliable in DEBUG builds???
    /// </summary>
    /// <returns>True if check does not fail.</returns>
    [Tested]
    public override bool Check()
    {
      bool retval = true;

      if (size > array.Length)
      {
        Console.WriteLine("Bad size ({0}) > array.Length ({1})", size, array.Length);
        return false;
      }

      for (int i = 0; i < size; i++)
      {
        if ((object)(array[i]) == null)
        {
          Console.WriteLine("Bad element: nu
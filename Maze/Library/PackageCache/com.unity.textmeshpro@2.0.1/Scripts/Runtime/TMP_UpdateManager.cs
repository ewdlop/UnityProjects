                                          2020.2.1f1 ž’’’      ’’f!ė59Ż4QĮóB   ķ          7  ’’’’                 ¦ ²                       E                    Ž  #                     . ,                     5   a                    Ž  #                     . ,                      r                    Ž  #      	               . ,      
               H « ’’’’             1  1  ’’’’   @           Ž                     Q  j                    ń  J   ’’’’    Ą           1  1  ’’’’                Ž                        j  ’’’’                \     ’’’’                H r   ’’’’               1  1  ’’’’   @            Ž                      Q  j                     H w   ’’’’               1  1  ’’’’   @            Ž                      Q  j                     H    ’’’’               1  1  ’’’’   @            Ž                      Q  j                     y 
                      Ž  #      !               . ,      "                   ’’’’#   @          1  1  ’’’’$               Ž      %               . j     &               Õ    ’’’’'               1  1  ’’’’(    Ą            Ž      )                  j  ’’’’*                H   ’’’’+               1  1  ’’’’,   @            Ž      -                Q  j     .                y 
    /                 Ž  #      0               . ,      1                 §      2    @            ¾ ¶      3    @            Ž  #      4               . ,      5               H »   ’’’’6              1  1  ’’’’7   @            Ž      8                Q  j     9                H Ę   ’’’’:              1  1  ’’’’;   @            Ž      <                Q  j     =                H Ų   ’’’’>              1  1  ’’’’?   @            Ž      @                Q  j     A              MonoImporter PPtr<EditorExtension> m_FileID m_PathID PPtr<PrefabInstance> m_ExternalObjects SourceAssetIdentifier type assembly name m_UsedFileIDs m_DefaultReferences executionOrder icon m_UserData m_AssetBundleName m_AssetBundleVariant     s    ’’£Gń×ÜZ56 :!@iĮJ*          7  ’’’’                 ¦ ²                        E                    Ž                       .                      (   a                    Ž                       .                       r                    Ž        	               .       
               H « ’’’’             1  1  ’’’’   @           Ž                     Q  j                    H ź ’’’’              1  1  ’’’’   @            Ž                      Q  j                     ń  =   ’’’’              1  1  ’’’’               Ž                       j  ’’’’               H   ’’’’              1  1  ’’’’   @            Ž                      Q  j                     y 
                    Ž                       .                      y Q                       Ž                       .                       Ž  X      !                H i   ’’’’"              1  1  ’’’’#   @            Ž      $                Q  j     %                H u   ’’’’&              1  1  ’’’’'   @            Ž      (                Q  j     )              PPtr<EditorExtension> m_FileID m_PathID PPtr<PrefabInstance> m_DefaultReferences m_Icon m_ExecutionOrder m_ClassName m_Namespace                       \       ąyÆ     `       Ø                                                                                                                                            ąyÆ                                                                                 	   IPlatform   B  using System;
using System.Collections.Generic;
using UnityEngine.Advertisements.Events;
using UnityEngine.Advertisements.Utilities;

namespace UnityEngine.Advertisements.Platform
{
    internal interface IPlatform
    {
        event EventHandler<StartEventArgs> OnStart;
        event EventHandler<FinishEventArgs> OnFinish;

        IBanner Banner { get; }
        IUnityLifecycleManager UnityLifecycleManager { get; }
        INativePlatform NativePlatform { get; }

        bool IsInitialized { get; }
        bool IsShowing { get; }
        string Version { get; }
        bool DebugMode { get; set; }

        HashSet<IUnityAdsListener> Listeners { get; }

        void Initialize(string gameId, bool testMode, bool enablePerPlacementLoad);
        void Load(string placementId);
        void Show(string placementId, ShowOptions showOptions);

        void AddListener(IUnityAdsListener listener);
        void RemoveListener(IUnityAdsListener listener);

        bool IsReady(string placementId);
        PlacementState GetPlacementState(string placementId);
        void SetMetaData(MetaData metaData);

        void UnityAdsReady(string placementId);
        void UnityAdsDidError(string message);
        void UnityAdsDidStart(string placementId);
        void UnityAdsDidFinish(string placementId, ShowResult rawShowResult);
    }
}
                      	   IPlatform                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ¾ ^¾  ^¾ Ą^¾ ą^¾  _¾  _¾ @_¾ `_¾ _¾  _¾ Ą_¾ ą_¾  `¾  `¾ @`¾ ``¾ `¾  `¾ Ą`¾ ą`¾  a¾  a¾ @a¾ `a¾ a¾  a¾ Ąa¾ ąa¾  b¾  b¾ @b¾ `b¾ b¾  b¾ Ąb¾ ąb¾  c¾  c¾ @c¾ `c¾ c¾  c¾ Ąc¾ ąc¾  d¾  d¾ @d¾ `d¾ d¾  d¾ Ąd¾ ąd¾  e¾  e¾ @e¾ `e¾ e¾  e¾ Ąe¾ ąe¾  f¾  f¾ @f¾ `f¾ f¾  f¾ Ąf¾ ąf¾  g¾  g¾ @g¾ `g¾ g¾  g¾ Ąg¾ ąg¾  h¾  h¾ @h¾ `h¾ h¾  h¾ Ąh¾ ąh¾  i¾  i¾ @i¾ `i¾ i¾  i¾ Ąi¾ ąi¾  j¾  j¾ @j¾ `j¾ j¾  j¾ Ąj¾ ąj¾  k¾  k¾ @k¾ `k¾ k¾  k¾ Ąk¾ ąk¾  l¾  l¾ @l¾ `l¾ l¾  l¾ Ąl¾ ąl¾  m¾  m¾ @m¾ `m¾ m¾  m¾ Ąm¾ ąm¾  n¾  n¾ @n¾ `n¾ n¾  n¾ Ąn¾ ąn¾  o¾  o¾ @o¾ `o¾ o¾  o¾ Ąo¾ ąo¾  p¾  p¾ @p¾ `p¾ p¾  p¾ Ąp¾ ąp¾  q¾  q¾ @q¾ `q¾ q¾  q¾ Ąq¾ ąq¾  r¾  r¾ @r¾ `r¾ r¾  r¾ Ąr¾ ąr¾  s¾  s¾ @s¾ `s¾ s¾  s¾ Ąs¾ ąs¾  t¾  t¾ @t¾ `t¾ t¾  t¾ Ąt¾ ąt¾  u¾  u¾ @u¾ `u¾ u¾  u¾ Ąu¾ ąu¾  v¾  v¾ @v¾ `v¾ v¾  v¾ Ąv¾ ąv¾  w¾  w¾ @w¾ `w¾ w¾  w¾ Ąw¾ ąw¾  x¾  x¾ @x¾ `x¾ x¾  x¾ Ąx¾ ąx¾  y¾  y¾ @y¾ `y¾ y¾  y¾ Ąy¾ ąy¾  z¾  z¾ @z¾ `z¾ z¾  z¾ Ąz¾ ąz¾  {¾  {¾ @{¾ `{¾ {¾  {¾ Ą{¾ ą{¾  |¾  |¾ @|¾ `|¾ |¾  |¾ Ą|¾ ą|¾  }¾  }¾ @}¾ `}¾ }¾  }¾ Ą}¾ ą}¾  ~¾  ~¾ @~¾ `~¾ ~¾  ~¾ Ą~¾ ą~¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `¾ ¾  ¾ Ą¾ ą¾  ¾  ¾ @¾ `
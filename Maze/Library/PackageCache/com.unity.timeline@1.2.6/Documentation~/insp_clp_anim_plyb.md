      S              u              �              �              �              �              �   	             
                        '             9             N             a             w             �             �             �             �         .text$mn              .B+�     D�jJ              .debug$S       l              � �              .text$mn       $  
   ��N}     � �              .debug$S       h             (xS                                                 0               C               h              ~               �               �               �              �  ?       $LN20           .xdata                ���>                          �          .pdata      	         gK�    U �                  �      	    .data       
          VcX                                 
    .rdata                �.rh                           =          .rdata         >       ��w�                           g          .rdata         Y       ?�w                           �          .rtc$IMZ                      K�p�                  �          .rtc$TMZ                      �Ϊ                  �          .debug$T       �                                   .chks64        �                                   	  __1FEB9909_corecrt_memcpy_s@h __9200769A_corecrt_wstring@h __32E5F013_string@h __A2143F22_corecrt_stdio_config@h __829E1958_corecrt_wstdio@h __6DFAE8B8_stdio@h __B2D2BA86_ctype@h __20BB4341_malloc@h __C6E16F6F_corecrt_wconio@h __1157D6BA_corecrt_wtime@h __1DC1E279_stat@h __93DC0B45_wchar@h __10BEA919_glib@h __79C7FC57_basetsd@h __D5DDFBF3_winnt@h __D4435474_winerror@h __B3ED30D4_winbase@h __A118E6DC_stralign@h __741AE07E_corecrt_math@h __F9B5D9B3_mono-os-semaphore@h __842FA901_mono-os-semaphore-win32@c monoeg_g_log mono_assertion_message_unreachable __imp_GetLastError mono_win32_wait_for_single_object_ex mono_os_sem_timedwait _RTC_InitBase _RTC_Shutdown __CheckForDebuggerJustMyCode __JustMyCode_Default $retry$21 $unwind$mono_os_sem_timedwait $pdata$mono_os_sem_timedwait ?__LINE__Var@?0??mono_os_sem_timedwait@@9@9 ??_C@_0BG@DPKEGAJB@mono_os_sem_timedwait@ ??_C@_0DO@FHOMLDCF@?$CFs?3?5mono_win32_wait_for_single_@ ??_C@_0FJ@FCOIHADI@D?3?2Coding?2mono?9mono?96?412?40?4114?2@ _RTC_InitBase.rtc$IMZ _RTC_Shutdown.rtc$TMZ                                                                                                       -----------
# Auxiliary class: TabTextCtrl
# This is the temporary ExpandoTextCtrl created when you edit the text of a tab
# -----------------------------------------------------------------------------

class TabTextCtrl(ExpandoTextCtrl):
    """ Control used for in-place edit. """

    def __init__(self, owner, tab, page_index):
        """
        Default class constructor.
        For internal use: do not call it in your code!

        :param `owner`: the :class:`AuiNotebook` owning the tab;
        :param `tab`: the actual :class:`AuiTabCtrl` tab;
        :param integer `page_index`: the :class:`AuiTabContainer` page index for the tab.
        """

        self._owner = owner
        self._tabEdited = tab
        self._pageIndex = page_index
        self._startValue = tab.caption
        self._finished = False
        self._aboutToFinish = False
        self._currentValue = self._startValue

        x, y, w, h = self._tabEdited.rect

        wnd = self._tabEdited.control
        if wnd:
            x += wnd.GetSize()[0] + 2
            h = 0

        image_h = 0
        image_w = 0

        image = tab.bitmap

        if image.IsOk():
            image_w, image_h = image.GetWidth(), image.GetHeight()
            image_w += 6

        dc = wx.ClientDC(self._owner)
        h
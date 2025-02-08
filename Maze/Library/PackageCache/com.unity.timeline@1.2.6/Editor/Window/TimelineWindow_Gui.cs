//
// HandlerTransportBindingElement.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2005 Novell, Inc.  http://www.novell.com
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#if !MOBILE && !XAMMAC_4_5
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Threading;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace MonoTests.System.ServiceModel.Channels
{
	public delegate Message RequestSender (Message input);
	public delegate Message RequestReceiver ();
	public delegate void ReplyHandler (Message input);

	public class HandlerTransportBindingElement : TransportBindingElement
	{
		RequestSender sender;

		ReplyHandler reply_handler;
		RequestReceiver receiver;

		public HandlerTransportBindingElement (RequestSender sender)
		{
			this.sender = sender;
		}

		public HandlerTransportBindingElement (ReplyHandler handler, RequestReceiver receiver)
		{
			this.reply_handler = handler;
			this.receiver = receiver;
		}

		public RequestSender RequestSender {
			get { return sender; }
		}

		public ReplyHandler ReplyHandler {
			get { return reply_handler; }
		}

		public RequestReceiver RequestReceiver {
			get { return receiver; }
		}

		public override string Scheme {
			get { return "stream"; }
		}

		public override BindingElement Clone ()
		{
			if (sender != null)
				return new HandlerTransportBindingElement (sender);
			else
				return new HandlerTransportBindingElement (reply_handler, receiver);
		}

		public override bool CanBuildChannelFactory<TChannel> (BindingContext context)
		{
			return typeof (TChannel) == typeof (IRequestChannel) ||
				typeof (TChannel) == typeof (IRequestChannel);
		}

		public override bool CanBuildChannelListener<TChannel> (BindingContext context)
		{
			return typeof (TChannel) == typeof (IReplyChannel) ||
				typeof (TChannel) == typeof (IInputChannel);
		}

		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel> (BindingContext context)
		{
			return new HandlerTransportChannelFactory<TChannel> (this);
		}

		public override IChannelListener<TChannel> BuildChannelListener<TChannel> (BindingContext context)
		{
			// FIXME: pass uri
			return new HandlerTransportChannelListener<TChannel> (this, new Uri ("stream:dummy"));
		}
	}

	public class HandlerTransportChannelFactory<TChannel> : ChannelFactoryBase<TChannel>
	{
		HandlerTransportBindingElement source;

		public HandlerTransportChannelFactory (HandlerTransportBindingElement source)
		{
			this.source = source;
		}

		public HandlerTransportBindingElement Source {
			get { return source; }
		}

		protected override TChannel OnCreateChannel (EndpointAddress address, Uri via)
		{
			if (typeof (TChannel) == typeof (IRequestChannel))
				return (TChannel) (object) new HandlerTransportRequestChannel ((HandlerTransportChannelFactory<IRequestChannel>) (object) this, address, via);
			if (typeof (TChannel) == typeof (IOutputChannel))
				return (TChannel) (object) new HandlerTransportOutputChannel ((HandlerTransportChannelFactory<IOutputChannel>) (object) this, address, via);

			throw new NotSupportedException (String.Format ("Channel '{0}' is not supported.", typeof (TChannel)));
		}

		protected override IAsyncResult OnBeginOpen (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotSupportedException ();
		}

		protected override void OnEndOpen (IAsyncResult result)
		{
			throw new NotSupportedException ();
		}

		protected override void OnOpen (TimeSpan timeout)
		{
			// do nothing
		}
	}

	public class HandlerTransportOutputChannel : OutputChannelBase
	{
		HandlerTransportChannelFactory<IOutputChannel> source;
		EndpointAddress address;
		Uri via;

		public HandlerTransportOutputChannel (HandlerTransportChannelFactory<IOutputChannel> source, EndpointAddress address, Uri via)
			: base (source)
		{
			this.source = source;
			this.address = address;
			this.via = via;
		}

		public override EndpointAddress RemoteAddress {
			get { return address; }
		}

		public override Uri Via {
			get { return via; }
		}

		public override void Send (Message input, TimeSpan timeout)
		{
			source.Source.RequestSender (input);
		}

		class OutputAsyncResult : IAsyncResult
		{
			Message message;
			object state;
			bool completed = true;

			public OutputAsyncResult (Message message, object state)
			{
				this.message = message;
				this.state = state;
			}

			public Message Message {
				get { return message; }
			}

			public object AsyncState {
				get { return state; }
			}

			public WaitHandle AsyncWaitHandle {
				get { return null; }
			}

			public bool CompletedSynchronously {
				get { return true; }
			}

			public bool IsCompleted {
				get { return completed; }
				internal set { completed = value; }
			}
		}

		public override IAsyncResult BeginSend (Message input, TimeSpan timeout, AsyncCallback callback, object state)
		{
			// FIXME: timeout is not considered here.
			return new OutputAsyncResult (input, state);
		}

		public override void EndSend (IAsyncResult result)
		{
			source.Source.RequestSender (((OutputAsyncResult) result).Message);
		}


		protected override void OnAbort ()
		{
		}

		protected override void OnOpen (TimeSpan timeout)
		{
			// throw new NotImplementedException ("OnOpen");
		}

		protected override IAsyncResult OnBeginOpen (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginOpen");
		}

		protected override void OnEndOpen (IAsyncResult result)
		{
			throw new NotImplementedException ("OnEndOpen");
		}

		protected override void OnClose (TimeSpan timeout)
		{
			// throw new NotImplementedException ("OnClose");
		}

		protected override IAsyncResult OnBeginClose (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginClose");
		}

		protected override void OnEndClose (IAsyncResult result)
		{
			throw new NotImplementedException ("OnEndClose");
		}
	}

	public class HandlerTransportRequestChannel : RequestChannelBase
	{
		HandlerTransportChannelFactory<IRequestChannel> source;
		EndpointAddress address;
		Uri via;

		public HandlerTransportRequestChannel (HandlerTransportChannelFactory<IRequestChannel> source, EndpointAddress address, Uri via)
			: base (source)
		{
			this.source = source;
			this.address = address;
			this.via = via;
		}

		public override EndpointAddress RemoteAddress {
			get { return address; }
		}

		public override Uri Via {
			get { return via; }
		}

		public override Message Request (Message input, TimeSpan timeout)
		{
			return source.Source.RequestSender (input);
		}

		class RequestAsyncResult : IAsyncResult
		{
			Message message;
			object state;
			bool completed = true;

			public RequestAsyncResult (Message message, object state)
			{
				this.message = message;
				this.state = state;
			}

			public Message Message {
				get { return message; }
			}

			public object AsyncState {
				get { return state; }
			}

			public WaitHandle AsyncWaitHandle {
				get { return null; }
			}

			public bool CompletedSynchronously {
				get { return true; }
			}

			public bool IsCompleted {
				get { return completed; }
				internal set { completed = value; }
			}
		}

		public override IAsyncResult BeginRequest (Message input, TimeSpan timeout, AsyncCallback callback, object state)
		{
			// FIXME: timeout is not considered here.
			return new RequestAsyncResult (input, state);
		}

		public override Message EndRequest (IAsyncResult result)
		{
			return source.Source.RequestSender (((RequestAsyncResult) result).Message);
		}


		protected override void OnAbort ()
		{
		}

		protected override void OnOpen (TimeSpan timeout)
		{
			// throw new NotImplementedException ("OnOpen");
		}

		protected override IAsyncResult OnBeginOpen (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginOpen");
		}

		protected override void OnEndOpen (IAsyncResult result)
		{
			throw new NotImplementedException ("OnEndOpen");
		}

		protected override void OnClose (TimeSpan timeout)
		{
			// throw new NotImplementedException ("OnClose");
		}

		protected override IAsyncResult OnBeginClose (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginClose");
		}

		protected override void OnEndClose (IAsyncResult result)
		{
			throw new NotImplementedException ("OnEndClose");
		}
	}

	public class HandlerTransportChannelListener<TChannel>
		: ChannelListenerBase<TChannel>
		where TChannel : class, IChannel
	{
		HandlerTransportBindingElement source;
		Uri uri;

		public HandlerTransportChannelListener (HandlerTransportBindingElement source, Uri uri)
		{
			this.source = source;
			this.uri = uri;
		}

		public HandlerTransportBindingElement Source {
			get { return source; }
		}


		public override Uri Uri {
			get { return uri; }
		}

		protected override TChannel OnAcceptChannel (TimeSpan timeout)
		{
			if (typeof (TChannel) == typeof (IReplyChannel))
				return (TChannel) (object) new HandlerTransportReplyChannel ((HandlerTransportChannelListener<IReplyChannel>) (object) this);

			throw new NotSupportedException ();
		}

		protected override IAsyncResult OnBeginAcceptChannel (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginAcceptChannel");
		}

		protected override TChannel OnEndAcceptChannel (IAsyncResult result)
		{
			throw new NotImplementedException ("EndAcceptChannel");
		}

		protected override bool OnWaitForChannel (TimeSpan timeout)
		{
			return true;
		}

		protected override IAsyncResult OnBeginWaitForChannel (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginWaitForChannel");
		}

		protected override bool OnEndWaitForChannel (IAsyncResult result)
		{
			throw new NotImplementedException ("EndWaitForChannel");
		}


		protected override void OnAbort ()
		{
		}

		protected override void OnOpen (TimeSpan timeout)
		{
			// throw new NotImplementedException ("OnOpen");
		}

		protected override IAsyncResult OnBeginOpen (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginOpen");
		}

		protected override void OnEndOpen (IAsyncResult result)
		{
			throw new NotImplementedException ("EndOpen");
		}

		protected override void OnClose (TimeSpan timeout)
		{
			//throw new NotImplementedException ("Close");
		}

		protected override IAsyncResult OnBeginClose (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginClose");
		}

		protected override void OnEndClose (IAsyncResult result)
		{
			throw new NotImplementedException ("OnEndClose");
		}
	}

	public class HandlerTransportReplyChannel : ReplyChannelBase
	{
		EndpointAddress address;
		HandlerTransportChannelListener<IReplyChannel> source;

		public HandlerTransportReplyChannel (HandlerTransportChannelListener<IReplyChannel> source)
			: base (source)
		{
			this.source = source;
			address = new EndpointAddress (source.Uri);
		}

		public HandlerTransportChannelListener<IReplyChannel> Source {
			get { return source; }
		}

		public override EndpointAddress LocalAddress {
			get { return address; }
		}

		class ReceiveRequestAsyncResult : IAsyncResult
		{
			object state;
			bool completed = true;

			public ReceiveRequestAsyncResult (object state)
			{
				this.state = state;
			}

			public object AsyncState {
				get { return state; }
			}

			public WaitHandle AsyncWaitHandle {
				get { return null; }
			}

			public bool CompletedSynchronously {
				get { return true; }
			}

			public bool IsCompleted {
				get { return completed; }
				internal set { completed = value; }
			}
		}

		public override RequestContext ReceiveRequest (TimeSpan timeout)
		{
			RequestContext ret;
			if (!TryReceiveRequest (timeout, out ret))
				throw new Exception ();
			return ret;
		}

		public override IAsyncResult BeginReceiveRequest (TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReceiveRequestAsyncResult (state);
		}

		public override RequestContext EndReceiveRequest (IAsyncResult result)
		{
			//ReceiveRequestAsyncResult r =
			//	(ReceiveRequestAsyncResult) result;
			return new HandlerRequestContext (this);
		}

		public override bool TryReceiveRequest (TimeSpan timeout, out RequestContext ret)
		{
			ret = new HandlerRequestContext (this);
			return true;
		}

		public override IAsyncResult BeginTryReceiveRequest (TimeSpan timeout, AsyncCallback callback, object state)
		{
			// hack, hack
			return new ReceiveRequestAsyncResult (state);
		}

		public override bool EndTryReceiveRequest (IAsyncResult result, out RequestContext ret)
		{
			// hack, hack
			//ReceiveRequestAsyncResult r =
			//	(ReceiveRequestAsyncResult) result;
			return TryReceiveRequest (TimeSpan.FromSeconds (5), out ret);
		}

		public override bool WaitForRequest (TimeSpan timeout)
		{
			throw new NotImplementedException ();
		}

		public override IAsyncResult BeginWaitForRequest (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("BeginWaitForRequest");
		}

		public override bool EndWaitForRequest (IAsyncResult result)
		{
			throw new NotImplementedException ("EndWaitForRequest");
		}


		protected override void OnAbort ()
		{
		}

		protected override void OnOpen (TimeSpan timeout)
		{
			// throw new NotImplementedException ("OnOpen");
		}

		protected override IAsyncResult OnBeginOpen (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginOpen");
		}

		protected override void OnEndOpen (IAsyncResult result)
		{
			throw new NotImplementedException ("EndOpen");
		}

		protected override void OnClose (TimeSpan timeout)
		{
			//throw new NotImplementedException ("Close");
		}

		protected override IAsyncResult OnBeginClose (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("OnBeginClose");
		}

		protected override void OnEndClose (IAsyncResult result)
		{
			throw new NotImplementedException ("OnEndClose");
		}
	}

	public class HandlerRequestContext : RequestContext
	{
		HandlerTransportReplyChannel source;
		Message request_message;

		public HandlerRequestContext (HandlerTransportReplyChannel source)
		{
			this.source = source;
			if (source.Source.Source.RequestReceiver != null)
				request_message = source.Source.Source.RequestReceiver ();
		}


		public override void Abort ()
		{
		}

		public override void Close ()
		{
		}

		public override void Close (TimeSpan timeout)
		{
		}

		public override Message RequestMessage {
			get { return request_message; }
		}

		public override void Reply (Message msg)
		{
			source.Source.Source.ReplyHandler (msg);
		}

		public override void Reply (Message msg, TimeSpan timeout)
		{
			source.Source.Source.ReplyHandler (msg);
		}

		public override IAsyncResult BeginReply (Message msg, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("BeginReply");
		}

		public override IAsyncResult BeginReply (Message msg, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ("BeginReply");
		}

		public override void EndReply (IAsyncResult result)
		{
			throw new NotImplementedException ("EndReply");
		}
	}
}
#endif
                                                                                                                                                                                                                                                                                                                   Ñá∞}&N1âôıÆ¿Tj#djêÃBG∂CXˇ–›n˜<Hrì≥ﬂ∏^ ‰ºLÁê^àª#–mz©lbú@mÿºA\{≤@	ˇ§áÃõ(ÉS·pªNı?'§°áµ·:w}#Uëˇ û=≈Î[∞ïÁ[c!˛õnY∂Ú¥÷!“jCh«∞‚Qﬂ°MUü
ÕH>Û≤*œOÊ¨î«˛Ub™ù∏‹Ò/ß‘©ÙözãíÒ;†a˙
ƒ¢f ÑÀ}¡´⁄O{„1˝˘»<{ ¯ÈüÂÚœ€Ú˛mˆ¢∞	c©=ÓºâØ±õ÷ìåà„Ã;∆Qöˇ>?f¡¸O‡)Ü_æpVò¨.Ol=ÈYhè´ºˇ≠\1πÔÆ€Œc‰Á{G˝i“Œ˘óÌ#] æ›G¸√î÷åRcîÂ_k∆≥Ê´¸’ø§±|pœ »ﬂåvG“Ω¿Yî<Ê◊-i\5K‚∆pÌ3R‡π’∆7ö§!ˆdÍ™ózˇj◊π 9¿π3GîíËAûN—g∑s€SX)D:…;¿MóŸI¥√ç-˝˘˚|
# MΩI˘Dó©îLO—-Í^F4º Ui≈∆Ÿ⁄`Í0"	l|È†6!WeÓ`uÆ;y˜fﬂ.Lòˇ>»
ÔOÈ≈èÉÉ¨√ÎfÕÏ∂Æ∏2v«¸Kô5∑ÂƒÙ≠hÜŸç„QZ mZ=NQπô⁄úﬁz÷À€RﬁŒ‰w*ŒEπc=Ì∆|ß)åúÜ2eµ∫<G§íÏK{)Ÿ˚´-à—∏E•∏w6¶⁄s”D6-`™ö-ïßœnë®Dî[ã«◊I<ô-Iá…Xq?#≥7=<∫˝r)∂÷©?ΩSo+’0A'Çƒƒ“" œ}»ÖqÏ{ˇã§ûH–w_CR⁄ÆÀZDÌ1e!,'[-√ó∂7k,u¡#4<T◊ıØ÷‘ƒº¯à	äH¸†oº‚/häÛ;œÓÿ‘X(Ó>31!Ì&ºÇ(¡Õ*øS„%ø¥˙HÁ„Ç.]≥æ†Ñ5Ü:ŸÈÚáys®ùú∆L2vÄ˜ÂÌYA•‘ßhzRQˆò£ŸÒÉß≥o«rbñxÖ jÉE¬ˇ ëã±I“k6ﬂsºÀÑ8Eín˜åóx¬Ó=≤	<hF»˚_Ñ§Ωú∞≥Íjˆ¢XøÑ—lπ¶ﬁm≤WHˆ¶i#ì”+
√F£(.≥áÃ'¿b1diÌVGçﬂ'%zã∞7Áä£|ø/Küî˛òXÃß¯hlIó≈ÕŒà„´˝ÿ|¢(1Ódﬂ¥£√,Ã∫é⁄|æp˜;Â¢¶hSõDuN‰≤nª_ﬁâCË √y^ßs∑—œ’ûTp)€ŒÁí´Öo@o'\ˆ√èXñ∏≈a⁄b¯$4í\	ÍS2Òë3ÛÙÉx∂œ#ñîöhôKßCÏÙPäâ¬SbO‡ù` ´Úõb}Õk˝˜∫´r∏]7˜_1£k≥sŸ.5íKã_˚çßñ¯ÈtˆHÄ
?sãÅŸ`e◊¿¥H¬~Éû=œB°&d·>◊g(ƒŒç`›–…13æ›÷nû0•R¸É#!¬ÉÛ4·õÕ`ÄÁÄHgÉò"n∑GY¶-“∑Ìzo˛-ïÊÉΩ£∂√–œô––ìùO¥„¢◊˚âÏ;ã†≥£ëW÷,/¯vÔ|qív	Ük–;> [åc~ÌÍÕ∏‡T~á{+ROaKdP≤>ÑøÇaâuUHÀ+Ã-„Îƒ_§t7ã∏ók»˛†4ïˆÄKZNà%∂¸39FÎÚdcÆ=/D˛º—&÷©∆∆g;ÿî√!ŸX{•Æu]jÂÂï]%Kß≥∏õÁé◊8UÆ˛µr^äç›¯h‰0:Æ≈¢LœÕ ô†eÎå÷<g1Ëù≈é(√∏J©›6ìn>Î“Sl”>Ó_ÒvZ‚IÒ*6˛%ï≠qä§Õñ$xÙè¨æJJÈƒ◊§˚bp∫¯k.“qihDm-sÌÕÚjÌ¨)±˝í˚Ë„õó(ªáwÅ‚©Üp%ô™önï-Ñ¥≤–œeÖåf¨UµíµHÄO™‘wŸ}S<.qì•otnå"˜âPıËì+î7;TIG‹ÑËN2w,6ÑiÖ¶æ1J€nvyÃzqïiúëD„·œ*9‰Èó€ 
†ÒòØìîÂ}≠/Z`≤ˇ[Â¶7˚öœ“]7[rÎ·Û£%É†XÏ“ëÌ`.X&Âò\#‹«X∞*WñY¬jø	›e–pøµkœè¯±!∂8 ëCq‹ûõ”(ÄFÜÆË]ùPYL¬ÒŒhº%r8˝ƒÃÛw‚≈[éïâ Y∏)∆ıFR¯pÓV§K«l£@WìS⁄Dw?©‘ê¬É|ÅLvOow˚æEì|ómÒπ§Ó33ÛÑ’	ïÏÓÛÙè*«≠-%g*—ä—!D!%…€«-Æ¬πbkf∏€Jó
îÂ6±©Fº˙µê¢jèîQ÷r2ãÄˇ¡Ù!)Bâ¥'4ê.a“ËôΩñ¥˙∑ˆéKÌó"å/œ)à ∑µèøî‹k]ª3—FÅòQ ÛdKÙÙ}ﬁ<¡2¥L’Ëæ.A|MæeY'∫*RM·◊⁄)ßã≈∫f≈dÒ\c£:í‹Ÿù˚±ıø,⁄æTD”OÏ&\·WÎ1ÒY¯pØÙÕë%∆ÒüÚ{£«◊U ÜΩ…«Ñö '†d€3IÑ î9n~;sò¡8ƒÔ¸â˝î‹àˆæS<=é”i√?öo!Êª˛“!>"&Q˝ÏÄ" ËÂÀ|\ˆﬁºë»~$Euõñ©Î⁄Û^Ù«‘ü,,ã—∏Ö=ıÒ@≠±‹ñ›bfã’¬.&≈iˆfés	t>ØNµ‘3cØ€—t3$ Ænº‡gÎD/EÒö.‡$∏“πîá®Ä!Ñù‡ö|˛!âIP#G[´Ü∞!Ÿ  ö†Ã¯îpf£úNﬂ^ÑöÇö‡ã»eV∞~#éÚ?‰ﬂÜ˜dçóôªPï°z* mwtVnä¨WuØé^@È ÉBê=qóÃÚ{¢î=4ìõ1ç7∏⁄AÃø–ªE'÷-ï¥—íË◊Fzq∏wLhÚº4 c/∞≠¶2¥iüà¯™‹G®H£àcÒ£+Î§†ŒQµÍ√ Y=˙∆R pYŒ‰¯^≤uÎIçO$Ñ‰ü◊#a˚¿H3éñ˛ˇW∏Ô≈ì|nÊà .:¸6»bògHFhåöV∂ˆ√‘?iÄ:*héIAˆÕMÍN[dÿ’ˇê-Ñ`O—K∫Ç,∆Gﬁq¸[‡Æ˘$ÍVcv"Â.ÙI∏X¸E™>:œ∂ü
ÁYÌÇˆXpÃµ¡∫B—˘#gäÿ8≥;Æ¿H∫+W∑ù<ø!ñ“Ì1B‚'ÃŸ›Ÿ‘¯–xºpÜqæŸ†Ωx3–Ë1;Â’T%éæ™&q0„lP+⁄Ó‘õë√Ü•,‘DˇÎ¶¿v™¥.¸‚ÿ≠√©˜p
3≥Í lMı€ŒqãX#¨Çºo0{“⁄ÈŒEqÒÃq^<rÒ÷*‡ÌR*3O(√πQØÖö∫›ÍmËÁb:í ®&î∞ñê≈’0-+¯>¬˘8ó˘@áU–÷¯/ÿÊ≠«π™öˇﬂbÎªÉ0ÿ35ãÄvÂ«j*3WÊú¯w‚*ü∂ÏH± ?∫’&Sof°ïkÿ∂d¯“åﬁè„Ø;Î.Ôìñ<ÀñçÂ˘ˇ}p1fì`åä¯èNàﬂ«7–ÂùF6⁄liÓIìy"ºáÊÊ£Áâp?-qõm1∏ÿ'¨øS*MïíD°3%⁄+™áñQdx9e´¶ƒ√ûW©≥J„ﬁ¢ÅÑ¥_téπ%Æ¸Hq\∞QËOS¿ö¨¨“Ë2Ä&MŸåìIfW Pöë*ÅØd`˘≈8&õ€lj’ÿ`4üC3¥ˇµ¥âé-≤∆†P\_πrÁT%Ÿ6¿ºú8_Ñmﬂ¶ÊrÓÌ!ÙÅ÷›up∂7QõçzÕE†;8èµWÿπ◊Ë‡ﬁëK {fª¡T5ëàWcû◊Îmy„Ìµï≤ÚHn1 Ò.%|1xw–;ÂΩÎ´Óˆ	'Ÿ£W0WƒaØ+KŸokªÕ‡üRsR®‚π˜R¶õﬁˇ˙-©oª‚{◊3—ª‰jlëø«;VƒædI8»˝d â¿ﬂ[Õ",lÙàd= »tÒù§ÇoF6^ù’0WGq¶ue∞ªÅÙ»zèí#˜S∂§bAè™%æÏ)AãäﬁæÊhìµÛU=†ﬂÒñXu/NqÍÀAR¨
PépaÅLïﬁFWq  SÎãÉàÕπ
ëW~‘Ó\ÄW`Û(-í≈ºb˛÷LÂ.≤Öf <Úàl-°˘¡y$J˛ﬁ‚ıùüÓçjq8<uö;$-ã6EŸ©∫o;áçSÈ„ˆ¸ Äøé∑Qs˙•ßÈì9†fM8∏1∞ìÿ+3ƒ7LFÖk˜Dv1¢T ÏÒÄÕµ<ò[ón7≥X≥⁄Z[#˝˛}J◊öõ¶ˆV®}‡7'ˇfåS3ˆ≠–Jé˜˜`lƒΩ⁄‰@~háuÇªÒ±M*‚˜ÂUÕ∆ãë+Ô®0¶Æaç|âÂo=ûÉ!òwRÇ¶¨JΩÔ˛Åî9á|y¶z’
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
                                                                                                                                                                                                                                                                                                                   ���}&N1�����Tj#dj��BG�CX���n�<Hr��߸^ �L��^��#�mz�lb�@mؼA\{�@	���̛(�S�p�N�?�'�����:w}#U��ʞ=��[���[c!��nY���!�jChǰ�QߡMU�
�H>�*�O欔��Ub�����/�ԩ��z���;�a�
Ģf ��}���O{�1���<{ ���������m���	c�=���֓����;�Q��>?f��O�)�_�pV��.Ol=�Yh�����\1����c��{G�i�����#] ��G�Ô֌Rc��_kƳ���տ��|p� �ߌvGҽ�Y�<��-i\5K��p�3R���7��!�dꪗz�j׹�9��3G���A�N��g�s�SX)D:�;�M��I�Í-���|
# M�I�D���LO�-�^F4��Ui����`�0"	l|�6!We�`u�;y�f�.L��>�
�O�ŏ�����f�춮�2v��K�5�����h�ٍ�QZ mZ=NQ��ڜ�z���R���w*�E�c=��|�)���2e��<G���K{)���-�ѸE��w6��s�D6-`��-���n��D�[���I<�-I��Xq?#�7=<��r)�֩?�So+�0A'����"��}ȅq��{����H�w_CRڮ�ZD�1e!,'[-×�7k,u�#4<T�����ļ��	�H��o��/h��;����X(�>31!��&��(��*�S�%���H��.]����5�:���ys����L2v����YA���hzRQ����񃧳o�rb�x��j�E��ʑ��I�k6�s�˄8E�n���x��=�	<hF��_�������j��X���l���m�WH��i#��+
�F�(.���'�b1di�VG��'%z��7���|�/K����X̧�hlI���Έ���|�(1�dߴ��,̺��|�p�;墦hS�DuN�n�_މC���y^�s���՞Tp)��璫�o@o'\�ÏX���a�b�$4�\	�S2�3���x��#���h�K�C��P���SbO��` ��b}�k����r�]7�_1�k�s�.5�K�_������t�H�
?s���`e���H�~��=�B�&d�>�g(�΍`���13���n�0�R��#!�4��`��Hg��"n�GY�-ҷ�zo�-�惽����ϙ�Г�O�������;�����W�,/�v�|q�v	�k�;>�[�c~��͸�T~�{+ROaKdP�>���a�uUH�+�-���_�t7���k���4���KZN�%��39F��dc�=/D���&֩��g;ؔ�!�X{��u]j��]%K������8U���r^�����h�0:���L�� ��e��<g1�Ŏ(��J��6�n>��Sl�>�_�vZ�I�*6�%��q��͖$x􏬾JJ�����bp��k.�qihDm-s���j�)�����㛗(��w�⩆p%���n�-�����e��f�U���H�O��w�}S<.q��otn�"��P��+�7;TIG���N2w,6�i���1J�nvy�zq�i��D���*9��� 
�񘯓��}�/Z`��[�7����]7[r���%��X�ґ�`.X&�\#��X�*W�Y�j�	�e�p��kϏ��!�8 �Cq����(�F���]�PYL���h�%r8����w��[����Y�)��FR�p�V�K�l�@W�S�Dw?�Ԑ|�LvOow��E�|�m���33��	�����*ǭ-%g*ъ�!D!%���-�¹bkf��J�
��6��F�����j��Q�r2�����!)B��'4�.a�虽������K�"��/�)� ������k]�3�F��Q��dK��}�<�2�L��.A|M�eY'�*RM���)��źf�d�\c�:��������,ھTD�O�&\�W�1�Y�p��͑%���{���U ���Ǆ� '�d�3I� �9n~;s���8�����������S<=��i�?�o!���!>"&Q��" ���|\�޼��~$Eu������^��ԟ,�,�Ѹ�=��@��ܖ�bf���.&�i�f�s	t>�N��3c���t�3$ �n��g�D/E�.�$�ҹ����!����|�!�IP#G[���!�� �����pf��N�^�������eV�~#��?�߆�d����P��z*�mwtVn��Wu��^@� �B�=q���{��=4��1�7��A̿лE'�-��ђ��Fzq�wLh�4�c/���2�i�����G�H��c�+뤠�Q��� Y=��R pY���^�u�I�O$���#a��H3����W��œ|n� .:�6�b�gHFh��V����?i�:*h�IA��M�N[d����-�`O�K��,�G�q�[��$�Vcv"�.�I�X�E�>:���
�Y��Xp̵��B��#g��8�;��H�+W���<�!���1B�'�������x�p�q�٠�x3��1;��T%���&q0�lP+��ԛ�Æ�,�D���v��.��حé�p
3�� lM���q��X#𬂼o0{����Eq��q^<r��*��R*3O(ùQ������m��b:� �&������0-+�>��8��@�U���/��ǹ����b뻃0�35��v��j*3W��w�*���H� ?��&Sof��kضd����ޏ��;�.<˖����}p1f�`����N���7��F6�li�I�y"�����p?-q�m1��'��S*M��D�3%�+���Qdx9e�����W��J�ޢ���_t��%��Hq\�Q�OS������2�&M���IfW�P��*��d`��8&��lj��`4�C3������-�ƠP\_�r�T%�6���8_�mߦ�r��!���up�7Q��z�E�;8��Wع���ޑK {f��T5��Wc���my����Hn1 �.%|1xw�;����	'٣W0W�a�+K�ok����RsR���R�����-�o��{�3ѻ�jl���;VľdI8��d ���[�",l�d=��t񝤂oF6^��0WGq�ue�����z��#�S��bA��%��)A��޾�h���U=���Xu/Nq��AR�
P�pa�L��FWq �S닃�͹
�W~��\�W`�(-�żb��L�.��f <�l-���y$J�������jq8<u�;$-�6E٩�o;��S���� ����Qs����9�fM8�1���+3�7LF�k�Dv1�T���͵<�[�n7�X��Z�[#��}Jך���V�}�7'�f�S3���J���`lĽ��@~h�u���M*���U�Ƌ�+�0��a�|��o=��!�wR���J�����9�|y�z�
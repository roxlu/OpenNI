using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace xn
{
	public class MockDepthGenerator : DepthGenerator
	{
		internal MockDepthGenerator(IntPtr nodeHandle, bool addRef) :
			base(nodeHandle, addRef)
		{
		}

		public MockDepthGenerator(Context context, string name) :
			this(Create(context, name), false)
		{
		}

		public MockDepthGenerator(Context context) :
			this(context, null)
		{
		}

		public MockDepthGenerator(DepthGenerator basedOn, string name) :
			this(CreateBasedOn(basedOn, name), false)
		{
		}

		public MockDepthGenerator(DepthGenerator basedOn) :
			this(basedOn, null)
		{
		}

		public void SetData(UInt32 frameID, UInt64 timestamp, UInt32 dataSize, IntPtr buffer)
		{
			UInt32 status = OpenNIImporter.xnMockDepthSetData(this.InternalObject, frameID, timestamp, dataSize, buffer);
			WrapperUtils.CheckStatus(status);
		}

		public void SetData(DepthMetaData depthMD, UInt32 frameID, UInt64 timestamp)
		{
			SetData(frameID, timestamp, depthMD.DataSize, depthMD.DepthMapPtr);
		}

		public void SetData(DepthMetaData depthMD)
		{
			SetData(depthMD, depthMD.FrameID, depthMD.Timestamp);
		}

		private static IntPtr Create(Context context, string name)
		{
			IntPtr handle;
			UInt32 status = OpenNIImporter.xnCreateMockNode(context.InternalObject, NodeType.Depth, name, out handle);
			WrapperUtils.CheckStatus(status);
			return handle;
		}

		private static IntPtr CreateBasedOn(DepthGenerator basedOn, string name)
		{
			IntPtr handle;
			UInt32 status = OpenNIImporter.xnCreateMockNodeBasedOn(basedOn.GetContext().InternalObject, 
				basedOn.InternalObject, name, out handle);
			WrapperUtils.CheckStatus(status);
			return handle;
		}
	}
}
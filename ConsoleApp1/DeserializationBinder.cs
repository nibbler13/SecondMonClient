using System;
using System.Runtime.Serialization;
using System.Reflection;

namespace SecondMonClient {
	sealed class DeserializationBinder : SerializationBinder {
		public override Type BindToType(string assemblyName, string typeName) {
			Type typeToDeserialize = null;
			String exeAssembly = Assembly.GetExecutingAssembly().FullName;
			typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
				typeName, exeAssembly));
			return typeToDeserialize;
		}
	}
}

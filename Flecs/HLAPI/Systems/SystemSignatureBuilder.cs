using System.Text;

namespace Flecs
{
	/// <summary>
	/// helper for building system signatures
	/// </summary>
	public struct SystemSignatureBuilder
	{
		StringBuilder _builder;

		/// <summary>
		/// This is the default SELF modifier, and is implied when no modifiers are explicitly specified
		/// </summary>
		public SystemSignatureBuilder With<T>() where T : unmanaged => Append(typeof(T).Name);

		/// <summary>
		/// This modifier is the same as the SELF modifier, but will only match entities that own the component
		/// </summary>
		public SystemSignatureBuilder WithOwned<T>() where T : unmanaged => Append("OWNED." + typeof(T).Name);

		/// <summary>
		/// This modifier is the same as the SELF modifier, but will only match entities that share the component from another entity
		/// </summary>
		public SystemSignatureBuilder WithShared<T>() where T : unmanaged => Append("SHARED." + typeof(T).Name);

		/// <summary>
		/// The CONTAINER modifier allows a system to select a component from the entity that contains the currently iterated over entity
		/// </summary>
		public SystemSignatureBuilder WithContainer<T>() where T : unmanaged => Append("CONTAINER." + typeof(T).Name);

		/// <summary>
		/// The CASCADE modifier is similar to an optional CONTAINER column, but in addition it ensures that entities are iterated
		/// over in the order of the container hierarchy. CASCADE columns are available to the system as optional shared columns.
		/// </summary>
		public SystemSignatureBuilder WithCascade<T>() where T : unmanaged => Append("CASCADE." + typeof(T).Name);

		/// <summary>
		/// In some cases it is useful to have stateful systems that either track progress in some way, or contain information pointing
		/// to an external source of data (like a database connection). The SYSTEM modifier allows for an easy way to access data
		/// associated with the system.
		/// </summary>
		public SystemSignatureBuilder WithSystem<T>() where T : unmanaged => Append("SYSTEM." + typeof(T).Name);

		/// <summary>
		/// In some cases, it is useful to get a component from a specific entity. In this case, the source modifier can specify
		/// the name of a named entity (that has the EcsId component) to obtain a component from that entity.
		/// </summary>
		public SystemSignatureBuilder WithEntity<T>(string entity) where T : unmanaged => Append(entity + "." + typeof(T).Name);

		/// <summary>
		/// The empty modifier lets an application pass handles to components or other systems to a system
		/// </summary>
		public SystemSignatureBuilder WithEmpty<T>() where T : unmanaged => Append("." + typeof(T).Name);

		/// <summary>
		/// The empty modifier lets an application pass handles to components or other systems to a system
		/// </summary>
		public SystemSignatureBuilder WithEmpty(string entityOrSystem) => Append("." + entityOrSystem);

		public SystemSignatureBuilder WithSingleton<T>() where T : unmanaged => Append("$." + typeof(T).Name);

		/// <summary>
		/// The NOT operator (!) allows the system to exclude entities that have a certain component
		/// </summary>
		public SystemSignatureBuilder Without<T>() where T : unmanaged => Append("!" + typeof(T).Name);

		/// <summary>
		/// The optional operator (?) allows a system to optionally match with a component
		/// </summary>
		public SystemSignatureBuilder OptionallyWith<T>() where T : unmanaged => Append("?" + typeof(T).Name);

		/// <summary>
		/// The OR operator (|) allows the system to match with one component in a list of components
		/// </summary>
		public SystemSignatureBuilder WithEither<T1, T2>() where T1 : unmanaged where T2 : unmanaged => Append(typeof(T1).Name + " | " + typeof(T2).Name);

		/// <summary>
		/// Appends the string verbatim to the system signature
		/// </summary>
		public SystemSignatureBuilder Append(string val)
		{
			if (_builder == null)
				_builder = new StringBuilder(30);

			if (_builder.Length == 0)
				_builder.Append(val);
			else
				_builder.Append($", {val}");

			return this;
		}

		public string Build() => _builder.ToString();
	}
}

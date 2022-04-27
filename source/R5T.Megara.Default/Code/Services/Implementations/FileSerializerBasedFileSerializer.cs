using System;

using R5T.T0064;


namespace R5T.Megara.Default
{
    /// <summary>
    /// An <see cref="IFileSerializer{T}"/> implementation based on forwarding all work to an <see cref="IFileSerializer{T}"/> service dependency.
    /// Useful as a base-class for decorated file serializers. For example, an ISolutionFileSerializer implementation class, SolutionFileSerializer, could derive from <see cref="FileSerializerBasedFileSerializer{T}"/> and use a stream serializer-based <see cref="IFileSerializer{T}"/> implementation.
    /// NOTE: Do not add this class to a DI-container as an <see cref="IFileSerializer{T}"/> since it will cause an infinite recursion.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ServiceImplementationMarker]
    public class FileSerializerBasedFileSerializer<T> : IFileSerializer<T>, IServiceImplementation
    {
        private IFileSerializer<T> FileSerializer { get; }


        public FileSerializerBasedFileSerializer(IFileSerializer<T> fileSerializer)
        {
            this.FileSerializer = fileSerializer;
        }

        public T Deserialize(string filePath)
        {
            var value = this.FileSerializer.Deserialize(filePath);
            return value;
        }

        public void Serialize(string filePath, T value, bool overwrite = true)
        {
            this.FileSerializer.Serialize(filePath, value, overwrite);
        }
    }
}

namespace Blocks.Util;

public record DoubleLLNode<T>
{
    public DoubleLLNode<T> Next { get; init; }
    public DoubleLLNode<T> Prev { get; init; }
    public T Data { get; init; }
}
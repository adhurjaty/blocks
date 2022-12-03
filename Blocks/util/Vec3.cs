namespace Blocks.Util;

// right-hand rule 3-D vector. X is to the right, Y is up and Z is pointing out of the screen
public record Vec3<T>
(
    T X,
    T Y,
    T Z
);
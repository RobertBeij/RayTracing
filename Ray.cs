public class Ray
{
    public Ray(){}

    public Ray(Vec3 origin, Vec3 direction) 
    {
        this.origin = origin;
        this.direction = direction;
    }

    public Vec3 origin{ get; set;}
    public Vec3 direction{ get; set;}


    public Vec3 At(double t) {
        return origin.Add(direction.Multiply(t));
    }

}
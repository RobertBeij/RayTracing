using System;
using point3 = Vec3;
using color = Vec3;

public class Vec3
{
    public double[] e;

    public Vec3()
    {
        e = new double[3];
        e[0] = 0;
        e[1] = 0;
        e[2] = 0;
    }

    public Vec3(double e1, double e2, double e3)
    {
        e = new double[3];
        e[0] = e1;
        e[1] = e2;
        e[2] = e3;
    }

    public double x() {
        return e[0];
    }

    public double y() {
        return e[1];
    }

    public double z() {
        return e[2];
    }


    public Vec3 Negative()
    {
        e[0] = -e[0];
        e[1] = -e[1];
        e[2] = -e[2];
        return new Vec3(e[0], e[1], e[2]);
    }

    public Vec3 Add(Vec3 otherVec) {
        return new Vec3(this.x() + otherVec.x(), this.y() + otherVec.y(), this.z() + otherVec.z());
    }

    public double LengthSquared() {
        return this.x() * this.x() + this.y() * this.y() + this.z() * this.z();
    }

    public double Length() {
        return Math.Sqrt(LengthSquared());
    }

    //Utility Functions
    
    public override String ToString() {
        return $"{this.x()} {this.y()} {this.z()}";
    }

    public Vec3 Subtract(Vec3 otherVec) {
        return new Vec3(this.x() - otherVec.x(), this.y() - otherVec.y(), this.z() - otherVec.z());
    }

    public Vec3 Multiply(Vec3 otherVec) {
        return new Vec3(this.x() * otherVec.x(), this.y() * otherVec.y(), this.z() * otherVec.z());
    }

    public Vec3 Multiply(double t) {
        return new Vec3(this.x() * t, this.y() * t, this.z() * t);
    }

    public Vec3 Divide(double t) {
        return this.Multiply(1 / t);
    }

    public double Dot(Vec3 otherVector) {
        return 
        (this.x() * otherVector.x()) +
        (this.y() * otherVector.y()) +
        (this.z() * otherVector.z());
    }

    public Vec3 Cross(Vec3 otherVector) {
        return new Vec3(
            e[1] * otherVector.e[2] - e[2] * otherVector.e[1],
            e[2] * otherVector.e[0] - e[0] * otherVector.e[2],
            e[0] * otherVector.e[1] - e[1] * otherVector.e[0]);
    }

    public Vec3 UnitVector() {
        return this.Divide(this.Length());
    }

    public static string WriteColor(Vec3 vector) {
        var r = (int)(255.999 * vector.x());
        var g = (int)(255.999 * vector.y());
        var b = (int)(255.999 * vector.z());

        return $"{r} {g} {b}";
    }
}
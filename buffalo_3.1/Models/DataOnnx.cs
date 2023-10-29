using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Security.Policy;

public class dataOnnx
{
    public float squarenorthsouth { get; set; }
    public float squareeastwest { get; set; }
    public float headdirection_E { get; set; }
    public float headdirection_W { get; set; }
    public float northsouth_N { get; set; }
    public float sex_F { get; set; }
    public float sex_M { get; set; }
    public float preservation_B { get; set; }
    public float preservation_Other { get; set; }
    public float preservation_S { get; set; }
    public float preservation_Skullonly { get; set; }
    public float preservation_W { get; set; }
    public float preservation_bonesandskull { get; set; }
    public float preservation_deteriorated { get; set; }
    public float preservation_disturbed { get; set; }
    public float preservation_fair { get; set; }
    public float preservation_good { get; set; }
    public float preservation_headlessskeleton { get; set; }
    public float preservation_poor { get; set; }
    public float preservation_poorlypreserved { get; set; }
    public float preservation_verydisturbed { get; set; }
    public float preservation_wrapped_bonesshowing { get; set; }
    public float eastwest_E { get; set; }
    public float eastwest_W { get; set; }
    public float adultsubadult_A { get; set; }
    public float adultsubadult_C { get; set; }
    public float area_NE { get; set; }
    public float area_NNW { get; set; }
    public float area_NW { get; set; }
    public float area_SE { get; set; }
    public float area_SW { get; set; }
    public float ageatdeath_A { get; set; }
    public float ageatdeath_C { get; set; }
    public float ageatdeath_I { get; set; }
    public float ageatdeath_N { get; set; }

    public Tensor<float> AsTensor()
    {
        float[] data = new float[]
        {
            squarenorthsouth, squareeastwest, headdirection_E, headdirection_W, northsouth_N, sex_F, sex_M,
            preservation_B, preservation_Other, preservation_S, preservation_Skullonly, preservation_W,
            preservation_bonesandskull, preservation_deteriorated, preservation_disturbed, preservation_fair,
            preservation_good, preservation_headlessskeleton, preservation_poor, preservation_poorlypreserved,
            preservation_verydisturbed, preservation_wrapped_bonesshowing, eastwest_E, eastwest_W, adultsubadult_A,
            adultsubadult_C, area_NE, area_NNW, area_NW, area_SE, area_SW, ageatdeath_A, ageatdeath_C, ageatdeath_I,
            ageatdeath_N
        };
        int[] dimensions = new int[] { 1, 35 };
        //return new DenseTensor<float>(data, dimensions);
        //string[] stringData = Array.ConvertAll(data, x => x.ToString());
        return new DenseTensor<float>(data, dimensions);
    }
}

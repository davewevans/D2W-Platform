namespace D2W.WebPortal.Helpers;

public class GenerateString
{
    public static string GenerateNewString(int stringLength)
    {
        string lowers = "abcdefghijklmnopqrstuvwxyz";
        string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string number = "0123456789";
        string all = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
        Random random = new Random();
        var Randomstring = "!";
        for (int i = 1; i <= stringLength; i++)
            Randomstring = Randomstring.Insert(
                random.Next(Randomstring.Length),
                all[random.Next(all.Length - 1)].ToString()
            );
        string generated = "!";
        //for (int i = 1; i <= lowercase; i++)
        //    generated = generated.Insert(
        //        random.Next(generated.Length),
        //        lowers[random.Next(lowers.Length - 1)].ToString()
        //    );

        //for (int i = 1; i <= uppercase; i++)
        //    generated = generated.Insert(
        //        random.Next(generated.Length),
        //        uppers[random.Next(uppers.Length - 1)].ToString()
        //    );

        //for (int i = 1; i <= numerics; i++)
        //    generated = generated.Insert(
        //        random.Next(generated.Length),
        //        number[random.Next(number.Length - 1)].ToString()
        //    );

        //return generated.Replace("!", string.Empty);
        return Randomstring.Replace("!", string.Empty);
    }
}


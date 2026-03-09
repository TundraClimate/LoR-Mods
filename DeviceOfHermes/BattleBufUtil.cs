using System.Diagnostics.CodeAnalysis;

namespace BattleBuf;

public static class BattleBufExtension
{
    public static bool TryGetBuf<T>(this BattleUnitModel? model, [NotNullWhen(true)] out T? buf)
        where T : BattleUnitBuf
    {
        var res = model?.bufListDetail?.GetActivatedBufList().Find(buf => buf is T);

        buf = res as T;

        return res is not null;
    }

    public static T GetBufAndInitIfNull<T>(this BattleUnitModel model, Func<T> bufMake)
        where T : BattleUnitBuf
    {
        if (!model.TryGetBuf<T>(out var buf))
        {
            var newBuf = bufMake();

            model.bufListDetail.AddBuf(newBuf);

            return newBuf;
        }

        return buf;
    }
}

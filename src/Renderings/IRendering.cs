namespace Renderings
{
    /// <summary>
    /// Base for a rendering model
    /// </summary>
    public interface IRendering
    {
        /// <summary>
        /// Allows renderings to be filtered if they shouldn't be displayed as a full page
        /// </summary>
        bool IsFullPage { get; }

        /// <summary>
        /// Simple rendering engine allowing custom views per rendering tag, for example a custom razor view
        /// </summary>
        /// <param name="renderTag"></param>
        /// <returns></returns>
        string GetPartialView(string renderTag = null);
    }
}
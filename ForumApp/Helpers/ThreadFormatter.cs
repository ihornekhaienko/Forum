using ForumApp.Interfaces;
using System;

namespace ForumApp.Helpers
{
    public class ThreadFormatter : IThreadFormatter
    {
        public string Prettify(string thread)
        {
            var threadWithSpaces = TransformSpaces(thread);
            var threadCodeFormatted = TransformCodeTags(threadWithSpaces);
            return threadCodeFormatted;
        }

        private static string TransformSpaces(string thread)
        {
            return thread.Replace(Environment.NewLine, "<br />");
        }

        private static string TransformCodeTags(string thread)
        {
            var head = thread.Replace("[code]", "<pre>");
            return head.Replace(@"[/code]", "</pre>");
        }
    }
}

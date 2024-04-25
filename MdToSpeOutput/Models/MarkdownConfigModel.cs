namespace MdToSpeOutput.Models;
using Markdig;

public static class MarkdownConfigModel
{
    public static MarkdownPipeline Pipeline { get; }

    static MarkdownConfigModel()
    {
        Pipeline = new MarkdownPipelineBuilder()
            .Use<GitHubUserProfileExtension>()
            .Build();
    }
}


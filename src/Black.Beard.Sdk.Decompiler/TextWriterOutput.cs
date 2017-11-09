using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bb.Sdk.Loggings.Exceptions
{

    using Bb.Sdk.Decompiler.ILAst;
    using Bb.Sdk.NRefactory.CSharp;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// TextWriterOutput
    /// </summary>
    public class TextWriterOutput : IOutputFormatter
    {
        private int cntInline = 0;
        private bool html;
        private int indentation;
        private bool mustbeClosed = false;
        private bool needsIndent = true;
        private int offset;
        private readonly TextWriter textWriter;
        bool isAtStartOfLine = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterOutput"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        /// <param name="offset">The offset.</param>
        /// <exception cref="System.ArgumentNullException">textWriter</exception>
        public TextWriterOutput(TextWriter textWriter, int offset)
        {
            this.html = true;
            this.offset = offset;
            if (textWriter == null)
            {
                throw new ArgumentNullException("textWriter");
            }
            this.textWriter = textWriter;
        }

        /// <summary>
        /// Ends the node.
        /// </summary>
        /// <param name="node">The node.</param>
        public virtual void EndNode(AstNode node)
        {
            if (this.html)
            {
                if ((this.cntInline == 1) && this.InLine(node))
                {
                    this.mustbeClosed = true;
                }
                if (node is SimpleType)
                {
                    this.textWriter.Write("</span>");
                }
            }
            else if ((this.cntInline == 1) && this.InLine(node))
            {
                this.End = this.textWriter.ToString().Length - this.Start;
                this.cntInline--;
            }
        }

        /// <summary>
        /// Indents this instance.
        /// </summary>
        public void Indent()
        {
            this.indentation++;
        }

        /// <summary>
        /// Ins the line.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private bool InLine(AstNode node)
        {
            foreach (object i2 in node.Annotations)
            {
                if (i2 is List<ILRange>)
                {
                    List<ILRange> ranges = i2 as List<ILRange>;
                    foreach (ILRange range in ranges)
                    {
                        if ((range.From <= this.offset) && (range.To > this.offset))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// News the line.
        /// </summary>
        public void NewLine()
        {
            this.textWriter.WriteLine(this.html ? "<br />" : "");
            this.needsIndent = true;
            isAtStartOfLine = true;
        }


        /// <summary>
        /// Opens the brace.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public void OpenBrace(BraceStyle style)
        {
            switch (style)
            {

                case BraceStyle.DoNotChange:
                case BraceStyle.EndOfLine:
                case BraceStyle.BannerStyle:
                    WriteIndentation();
                    if (!isAtStartOfLine)
                        textWriter.Write(' ');
                    textWriter.Write('{');
                    break;

                case BraceStyle.EndOfLineWithoutSpace:
                    WriteIndentation();
                    textWriter.Write('{');
                    break;

                case BraceStyle.NextLine:
                    if (!isAtStartOfLine)
                        NewLine();
                    WriteIndentation();
                    textWriter.Write('{');
                    break;

                case BraceStyle.NextLineShifted:
                    NewLine();
                    Indent();
                    WriteIndentation();
                    textWriter.Write('{');
                    NewLine();
                    return;

                case BraceStyle.NextLineShifted2:
                    NewLine();
                    Indent();
                    WriteIndentation();
                    textWriter.Write('{');
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Indent();
            NewLine();
        }

        /// <summary>
        /// Closes the brace.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public void CloseBrace(BraceStyle style)
        {
            switch (style)
            {
                case BraceStyle.DoNotChange:
                case BraceStyle.EndOfLine:
                case BraceStyle.EndOfLineWithoutSpace:
                case BraceStyle.NextLine:
                    Unindent();
                    WriteIndentation();
                    textWriter.Write('}');
                    isAtStartOfLine = false;
                    break;

                case BraceStyle.BannerStyle:
                case BraceStyle.NextLineShifted:
                    WriteIndentation();
                    textWriter.Write('}');
                    isAtStartOfLine = false;
                    Unindent();
                    break;

                case BraceStyle.NextLineShifted2:
                    Unindent();
                    WriteIndentation();
                    textWriter.Write('}');
                    isAtStartOfLine = false;
                    Unindent();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Spaces this instance.
        /// </summary>
        public void Space()
        {
            this.WriteIndentation();
            this.textWriter.Write(' ');
        }

        /// <summary>
        /// Starts the node.
        /// </summary>
        /// <param name="node">The node.</param>
        public virtual void StartNode(AstNode node)
        {
            if (this.html)
            {
                if (node is SimpleType)
                    this.textWriter.Write("<span class=\"simpleType\">");
                
                if ((this.cntInline == 0) && this.InLine(node))
                {
                    this.Start = this.textWriter.ToString().Length;
                    this.textWriter.Write("<span class=\"inLine\">");
                    this.cntInline++;
                }
            }
            else if ((this.cntInline == 0) && this.InLine(node))
            {
                this.Start = this.textWriter.ToString().Length;
                this.cntInline++;
            }
        }

        /// <summary>
        /// Unindents this instance.
        /// </summary>
        public void Unindent()
        {
            this.indentation--;
        }

        /// <summary>
        /// Writes the comment.
        /// </summary>
        /// <param name="commentType">Type of the comment.</param>
        /// <param name="content">The content.</param>
        public void WriteComment(CommentType commentType, string content)
        {
            this.WriteIndentation();
            if (this.html)
            {
                this.textWriter.Write("<span class=\"comment\">");
            }
            switch (commentType)
            {
                case CommentType.SingleLine:
                    this.textWriter.Write("//");
                    this.textWriter.WriteLine(content);
                    isAtStartOfLine = true;
                    break;

                case CommentType.MultiLine:
                    this.textWriter.Write("/*");
                    this.textWriter.Write(content);
                    this.textWriter.Write("*/");
                    isAtStartOfLine = false;
                    break;

                case CommentType.Documentation:
                    this.textWriter.Write("///");
                    this.textWriter.WriteLine(content);
                    isAtStartOfLine = true;
                    break;
            }
            if (this.html)
            {
                this.textWriter.Write("</span>");
            }
        }

        /// <summary>
        /// Writes the identifier.
        /// </summary>
        /// <param name="ident">The ident.</param>
        public void WriteIdentifier(string ident)
        {
            this.WriteIndentation();
            this.textWriter.Write(ident);
            isAtStartOfLine = false;
        }

        /// <summary>
        /// Writes the indentation.
        /// </summary>
        private void WriteIndentation()
        {
            if (this.needsIndent)
            {
                this.needsIndent = false;
                for (int i = 0; i < this.indentation; i++)
                    this.textWriter.Write(this.html ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" : "\t");
            }
        }

        /// <summary>
        /// Writes a keyword to the output.
        /// </summary>
        /// <param name="keyword"></param>
        public void WriteKeyword(string keyword)
        {
            this.WriteIndentation();
            if (this.html)
            {
                this.textWriter.Write("<span class=\"keyWord\">");
            }
            this.textWriter.Write(keyword);
            if (this.html)
            {
                this.textWriter.Write("</span>");
            }
            isAtStartOfLine = false;
        }

        /// <summary>
        /// Writes a token to the output.
        /// </summary>
        /// <param name="token"></param>
        public void WriteToken(string token)
        {
            this.WriteIndentation();
            if ((this.html && token.StartsWith("\"")) && token.EndsWith("\""))
            {
                this.textWriter.Write("<span class=\"text\">");
            }
            this.textWriter.Write(token);
            if ((this.html && token.StartsWith("\"")) && token.EndsWith("\""))
            {
                this.textWriter.Write("</span>");
            }
            if (this.mustbeClosed && (token == ";"))
            {
                this.End = this.textWriter.ToString().Length - this.Start;
                this.textWriter.Write("</span>");
                this.cntInline--;
                this.mustbeClosed = false;
            }
            isAtStartOfLine = false;
        }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public int End { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public int Start { get; set; }


        /// <summary>
        /// Writes the pre processor directive.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="argument">The argument.</param>
        public void WritePreProcessorDirective(PreProcessorDirectiveType type, string argument)
        {
            // pre-processor directive must start on its own line
            if (!isAtStartOfLine)
                NewLine();
            WriteIndentation();
            textWriter.Write('#');
            textWriter.Write(type.ToString().ToLowerInvariant());
            if (!string.IsNullOrEmpty(argument))
            {
                textWriter.Write(' ');
                textWriter.Write(argument);
            }
            NewLine();
        }
    
    }

}

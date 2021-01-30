using System;

namespace EcsRx.UnityEditor.Editor.EditorInputs
{
    public class ObjectEditorInput : SimpleEditorInput<Object>
    {
        protected override Object CreateTypeUI(string label, Object value)
        {
            return value;
        }
    }
}
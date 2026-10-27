
using Microsoft.Practices.CompositeUI.SmartParts;

namespace EdwardsIndexAppCommon
{
	public interface IShowInShell
	{
		void Show(IWorkspace navbarWorkspace, IWorkspace content);
	}
}

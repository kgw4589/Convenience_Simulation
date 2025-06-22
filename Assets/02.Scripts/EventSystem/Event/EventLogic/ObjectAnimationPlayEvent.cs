using UnityEngine;

/// <summary>
/// 오브젝트 애니메이션 실행
/// </summary>
public class ObjectAnimationPlayEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        Animator animator = eventElement.objectAnimator;
        
        if (eventElement.isCurrentCustomer)
        {
            animator = CustomerManager.Instance.currentCustomer.GetComponent<Animator>();
        }
        
        animator.SetTrigger(eventElement.animParamName);
    }
}

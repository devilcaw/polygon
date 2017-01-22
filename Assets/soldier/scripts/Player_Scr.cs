using UnityEngine;
using System.Collections;

public class Player_Scr : MonoBehaviour {

    Animator anim; //аниматор
    //CharacterController charContr; //character controller

    public Transform rightHandTarget; //цель для правой руки
    public Transform leftHandTarget; //цель для левой руки
    float handWeight; //вес рук (степень их преобладания над основной анимацией)
    public float handSpeed; //скорость перехода рук от обычного состояния к состоянию прицеливания

    public float lookIKWeight; //вес слежения за целью
    public float eyesWeight; //вес глаз
    public float headWeight; //вес головы
    public float bodyWeight; // вес тела
    public float clampWeight; //ограничения на поворот
    public Transform targetPos; //положение цели

    public float angularSpeed; //скорость поворота персонажа
    bool isPlayerRot; //поворачивается ли персонаж
    public float luft; //угол люфта при повороте

    public Transform leftHand; //левая рука персонажа
    public Transform leftHandAvatar; //аватар левой руки
    Transform[] leftHandTrans; //массив положений каждого элемента левой руки
    Transform[] leftHandAvatarTrans; //массив положений каждого элемента аватара левой руки

    public Transform rightHand; //правая рука персонажа
    public Transform rightHandAvatar; //аватар правой руки
    Transform[] rightHandTrans; //массив положений кажого элемента правой руки
    Transform[] rightHandAvatarTrans; //массив положений каждого элемента аватара правой руки


    void Start () { //что делаем при старте
        anim = GetComponent<Animator>(); //присваиваем аниматор
        //charContr = GetComponent<CharacterController>(); //присваиваем контроллер

        leftHandTrans = leftHand.GetComponentsInChildren<Transform>(); //заполняем массив элементами левой руки
        leftHandAvatarTrans = leftHandAvatar.GetComponentsInChildren<Transform>(); //заполняем массив элементами аватара левой руки

        rightHandTrans = rightHand.GetComponentsInChildren<Transform>(); //заполняем массив элементами правой руки
        rightHandAvatarTrans = rightHandAvatar.GetComponentsInChildren<Transform>(); //заполняем массив элементами аватара правой руки
    }
	
	void Update () { // что делаем каждый кадр
        float newHandWeight = 0f; //создаем локальную переменную с весом рук равным 0
        if (Input.GetMouseButton(1)) newHandWeight = 1f; //если нажата правая кнопка мыши, то вес равен 1
        handWeight = Mathf.Lerp(handWeight, newHandWeight, Time.deltaTime * handSpeed); //плавно изменяем вес до нужного

        float newRunWeight = 0f; //создаем локальную переменную с весом бега равным 0
        if (Input.GetKey(KeyCode.LeftShift)) newRunWeight = 1f; //если нажат шифт, то вес равен 1
        anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), newRunWeight, Time.deltaTime * 5)); //плавно меняем вес до нужного

        //для клавиатуры
        /*
        int walk = Mathf.RoundToInt(Input.GetAxis("Vertical")); 
        int strafe = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        anim.SetInteger("Walk", walk);
        anim.SetInteger("Strafe", strafe);
        */
        //для геймпада

        float walk = Input.GetAxis("Vertical"); //присваиваем переменной вертикальную ось
        float strafe = Input.GetAxis("Horizontal"); //присваиваем переменной горизонтальную ось
        anim.SetFloat("Walk", walk); //меняем переменную в аниматоре
        anim.SetFloat("Strafe", strafe); //меняем переменную в аниматоре

        Vector3 rot = transform.eulerAngles; //запоминаем угол поворота персонажа
        transform.LookAt(targetPos.position); //поворачиваем персонажа на цель (это не увидит никто)
        
        float angleBetween = Mathf.DeltaAngle(transform.eulerAngles.y, rot.y); //угол между предыдущим поворотом персонажа и тем, который будет при слежкее за целью
        if ((Mathf.Abs(angleBetween) > luft)||((walk != 0) || strafe != 0)) //если персонаж двигается или угол больше чем люфт, то...
        {
            isPlayerRot = true; //переменная поворота равна true
        }
        if (isPlayerRot == true) //если переменная поворота равна true, то...
        {
            float bodyY = Mathf.LerpAngle(rot.y, transform.eulerAngles.y, Time.deltaTime * angularSpeed);//объявляем переменную, плавно меняющуюся от предыдущего поворота до нынешнего
            transform.eulerAngles = new Vector3(rot.x, bodyY, rot.z); //присваиваем персонажу новый угол поворота по оси Y
            
            if ((walk == 0) && (strafe == 0)) //если персонаж не двигается, то...
            {
                anim.SetBool("Turn", true); //проигрываем анимацию поворота
            }
            else {
                anim.SetBool("Turn", false); //иначе не проигрываем :)
            }
            
            if (Mathf.Abs(angleBetween) * Mathf.Deg2Rad <= Time.deltaTime * angularSpeed) //если угол между персонажем и целью меньше скорости поворота, то...
            {
                isPlayerRot = false; //прекращаем поворот
                anim.SetBool("Turn", false); //отключаем анимацию поворота
            }
        }
        else { 
            transform.eulerAngles = rot; //если переменная isPlayerRot равна false, то оставляем старый угол 
        }
        
    }

    void LateUpdate() { //действия в конце каждого кадра
        for (int i = 1; i < leftHandTrans.Length; i ++) { //для каждого элемента левой руки...
            leftHandTrans[i].localEulerAngles = leftHandAvatarTrans[i].localEulerAngles; //меняем угол на тот, который у аватара
        }
        for (int i = 1; i < rightHandTrans.Length; i++) // для каждого жлемента правой руки
        {
            rightHandTrans[i].localEulerAngles = rightHandAvatarTrans[i].localEulerAngles;//меняем угол на тот, который у аватара
        }
    }

    void OnAnimatorIK() //действия с аватаром (который на аниматоре)
    {
        anim.SetLookAtWeight(lookIKWeight, bodyWeight, headWeight, eyesWeight, clampWeight); //присваиваем новые веса и ограничения для головы, тела и глаз
        anim.SetLookAtPosition(targetPos.position); //присваиваем цель, за которой следить

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight); //меняем вес для позиции правой руки
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position); //меняем цель для позиции правой руки

        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight); //меняем вес для поворота правой руки
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation); //меняем цель для поворота правой руки

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, handWeight);//меняем вес для позиции левой руки
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);//меняем цель для позиции левой руки

        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, handWeight);//меняем вес для поворота левой руки
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);//меняем цель для поворота левой руки
    }
}

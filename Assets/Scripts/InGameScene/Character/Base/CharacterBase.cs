// yu-ki-rohi
// キャラクターの雛形


using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected int _currentHp;
    [SerializeField] protected CharacterData _data;

    [SerializeField] private GameObject explosion;

    [SerializeField] private SkinnedMeshRenderer[] _skinnedRenderers;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private Color _damageFlushColor = Color.white;
    private Material[] _flushMaterials;
    [SerializeField] private float _intensity = 3.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _returnIntensityRatio = 0.9f;

    public int CurrentHp { get { return _currentHp; } }
    public int MaxHp { get { return _data.MAXHP; } }

    public int Atk { get { return _data.ATK; } }
    public int AtkSub01 { get { return _data.ATK_SUB1; } }
    public int AtkSub02 { get { return _data.ATK_SUB2; } }
    public int AtkSpecial { get { return _data.ATK_SPECIAL; } }
    public float CoolTime { get { return _data.COOLTIME; } }
    public float Agi { get { return _data.AGI; } }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (_data != null)
        {
            _currentHp = _data.MAXHP;
        }

        int materialNum = _skinnedRenderers.Length + _meshRenderers.Length;
        _flushMaterials = new Material[materialNum];
       
        for(int i = 0; i < _skinnedRenderers.Length; i++)
        {            
            _flushMaterials[i] = _skinnedRenderers[i].materials[1];
        }

        for (int i = _skinnedRenderers.Length; i < materialNum; i++)
        {
            _flushMaterials[i] = _meshRenderers[i - _skinnedRenderers.Length].materials[1];
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // デバッグ用----------------------------------------------------------
        if (UnityEngine.Input.GetKey(KeyCode.Space))
        {
            DamageFlush(_damageFlushColor);
        }

        //--------------------------------------------------

        for (int i = 0; i < _skinnedRenderers.Length; i++)
        {
            if (_flushMaterials[i] != null)
            {
                Color finalColor = _flushMaterials[i].GetColor("_EmissionColor") * Mathf.LinearToGammaSpace(_returnIntensityRatio);
                _flushMaterials[i].SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(_skinnedRenderers[i], finalColor);
            }

        }
        for (int i = _skinnedRenderers.Length; i < _skinnedRenderers.Length + _meshRenderers.Length; i++)
        {
            if (_flushMaterials[i] != null)
            {
                Color finalColor = _flushMaterials[i].GetColor("_EmissionColor") * Mathf.LinearToGammaSpace(_returnIntensityRatio);
                _flushMaterials[i].SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(_meshRenderers[i - _skinnedRenderers.Length], finalColor);
            }
            
        }

    }

    // ダメージを受ける処理
    // 
    virtual public void Damage(int power, GameObject attacker, bool isCaptureBullet = false)
    {        
        // HP減少処理
        if(_currentHp > 0)
        {
            _currentHp -= power;
            if (_currentHp <= 0)
            {
                _currentHp = 0;
                Die(attacker, isCaptureBullet);
            }
            else
            {
                DamageFlush(_damageFlushColor);
            }
            Debug.Log(_currentHp);
        }          
    }

    virtual protected void Die(GameObject attacker, bool isCaptureBullet = false)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void DamageFlush(Color color)
    {
        for (int i = 0; i < _skinnedRenderers.Length; i++)
        {
            if (_flushMaterials[i] != null)
            {
                Color finalColor = color * Mathf.LinearToGammaSpace(_intensity);
                _flushMaterials[i].SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(_skinnedRenderers[i], finalColor);
            }
        }
        for (int i = _skinnedRenderers.Length; i < _skinnedRenderers.Length + _meshRenderers.Length; i++)
        {
            if (_flushMaterials[i] != null)
            {
                Color finalColor = color * Mathf.LinearToGammaSpace(_intensity);
                _flushMaterials[i].SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(_meshRenderers[i - _skinnedRenderers.Length], finalColor);
            }
            
        }
    }
}


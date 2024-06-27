// yu-ki-rohi
// キャラクターの雛形


using UnityEngine;
using UnityEngine.Windows;

public class CharacterBase : MonoBehaviour
{
    protected int _currentHp;
    [SerializeField] protected CharacterData _data;

    [SerializeField] private GameObject explosion;

    [SerializeField] private SkinnedMeshRenderer[] _skinnedRenderers;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private Color _damageFlushColor = Color.white;
    private Material[] _flushMaterials;
    private float _intensity;
    [SerializeField, Range(0.0f, 1.0f)] private float _returnIntensityRatio = 0.9f;

    public int CurrentHp { get { return _currentHp; } }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (_data != null)
        {
            _currentHp = _data.MAXHP;
        }

        int materialNum = _skinnedRenderers.Length + _meshRenderers.Length;
        _flushMaterials = new Material[materialNum];
        Debug.Log(_skinnedRenderers.Length + _meshRenderers.Length);
        for(int i = 0; i < _skinnedRenderers.Length; i++)
        {
            Debug.Log(i);
            _flushMaterials[i] = _skinnedRenderers[i].materials[1];
        }

        for (int i = _skinnedRenderers.Length; i < materialNum; i++)
        {
            Debug.Log(i);
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
    // 威力を取得するために、引数でColliderを渡してください
    protected void Damage(Collider collider)
    {
        // colliderの威力を受け取る処理
        // ここではとりあえず仕様書に則り10
        int damage = 10;

        // HP減少処理
        _currentHp -= damage;
        if(_currentHp < 0)
        {
            _currentHp = 0;
        }

        DamageFlush(_damageFlushColor);
        
    }

    private void DamageFlush(Color color)
    {
        for (int i = 0; i < _skinnedRenderers.Length; i++)
        {
            if (_flushMaterials[i] != null)
            {
                _intensity = 3.0f;
                Color finalColor = color * Mathf.LinearToGammaSpace(_intensity);
                _flushMaterials[i].SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(_skinnedRenderers[i], finalColor);
            }
        }
        for (int i = _skinnedRenderers.Length; i < _skinnedRenderers.Length + _meshRenderers.Length; i++)
        {
            if (_flushMaterials[i] != null)
            {
                _intensity = 3.0f;
                Color finalColor = color * Mathf.LinearToGammaSpace(_intensity);
                _flushMaterials[i].SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(_meshRenderers[i - _skinnedRenderers.Length], finalColor);
            }
            
        }
    }
}

